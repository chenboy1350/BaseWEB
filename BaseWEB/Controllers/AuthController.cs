using BaseWEB.Data.Entities;
using BaseWEB.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BaseWEB.Controllers
{
    public class AuthController(IUserService userService, ICookieAuthService cookieAuthService) : Controller
    {
        private readonly IUserService _userService = userService;
        private readonly ICookieAuthService _cookieAuthService = cookieAuthService;

        public IActionResult Login()
        {
            var rememberedUsername = Request.Cookies["RememberedUsername"];
            var rememberMeChecked = Request.Cookies["RememberMeChecked"] == "true";

            ViewBag.RememberedUsername = rememberedUsername;
            ViewBag.RememberMeChecked = rememberMeChecked;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, bool remember)
        {
            Userid user = _userService.ValidateUser(username, password);
            if (user == null || user.Num == 0)
            {
                return Json(new { success = false, message = "Invalid credentials" });
            }

            await _cookieAuthService.SignInAsync(HttpContext, user.Num, user.Userid1, user.Useridgroup, remember);

            if (remember)
            {
                Response.Cookies.Append("RememberedUsername", username, new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(7),
                    IsEssential = true
                });

                Response.Cookies.Append("RememberMeChecked", "true", new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(7),
                    IsEssential = true
                });
            }
            else
            {
                Response.Cookies.Delete("RememberedUsername");
                Response.Cookies.Delete("RememberMeChecked");
            }

            return Json(new
            {
                success = true,
                redirectUrl = Url.Action("Index", "Home")
            });
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _cookieAuthService.SignOutAsync(HttpContext);
            return Json(new
            {
                success = true,
                redirectUrl = Url.Action("Login", "Auth")
            });
        }

    }
}
