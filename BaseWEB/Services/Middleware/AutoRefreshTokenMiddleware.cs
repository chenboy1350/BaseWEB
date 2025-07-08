using BaseWEB.Services.Interface;

namespace BaseWEB.Services.Middleware
{
    public class AutoRefreshTokenMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
    {
        private readonly RequestDelegate _next = next;
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public async Task InvokeAsync(HttpContext context)
        {
            // เช็คว่า user login แล้วหรือยัง
            if (context.User.Identity?.IsAuthenticated == true)
            {
                // เช็คว่า Cookie Authentication หมดอายุเกือบหรือยัง
                var authTime = context.User.FindFirst("AuthTime")?.Value;
                if (authTime != null && DateTime.TryParse(authTime, out var authDateTime))
                {
                    // ถ้าหมดอายุใน 5 นาที ให้ refresh
                    if (authDateTime.AddMinutes(10) < DateTime.UtcNow)
                    {
                        using var scope = _serviceProvider.CreateScope();
                        var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();

                        var refreshResult = await authService.RefreshTokenAsync();
                        if (!refreshResult.Success)
                        {
                            // ถ้า refresh ไม่สำเร็จ ให้ logout
                            await authService.LogoutAsync();
                            context.Response.Redirect("/login");
                            return;
                        }
                    }
                }
            }

            await _next(context);
        }
    }
}
