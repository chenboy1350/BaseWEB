using System;
using System.Collections.Generic;
using BaseWEB.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BaseWEB.Data;

public partial class JPDbContext : DbContext
{
    public JPDbContext(DbContextOptions<JPDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Userid> Userid { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Userid>(entity =>
        {
            entity.HasKey(e => e.Num).IsClustered(false);

            entity.HasIndex(e => new { e.Userid1, e.Password, e.Useridgroup }, "IX_Userid")
                .IsUnique()
                .IsClustered()
                .HasFillFactor(90);

            entity.Property(e => e.Description).HasDefaultValue("");
            entity.Property(e => e.Mdate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Password).HasDefaultValue("");
            entity.Property(e => e.Useridgroup).HasDefaultValue("");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
