using System;
using DAL.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL {
    public partial class MlContext : DbContext {
        public MlContext() { }

        public MlContext(DbContextOptions<MlContext> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                // Remember kids, don't commit credentials
                optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=;database=movie_night");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity<Submission>().Property(e => e.Time).HasDefaultValueSql("now()");
            modelBuilder.Entity<Vote>().Property(e => e.Time).HasDefaultValueSql("now()");
            modelBuilder.Entity<User>().Property(e => e.Joined).HasDefaultValueSql("now()");
            modelBuilder.Entity<Submission>().Property(e => e.IsWatched).HasDefaultValue(false);

            // Seed accounts
            modelBuilder.Entity<User>().HasData(
                new User {
                    Id = 1,
                    Username = "admin",
                    Secret = "AQAAAAEAACcQAAAAEJF5fNmVVy9dOuPkPazUujaDPAm5biKWwwVc0lH/fS9+t1Ixpqsy1klZ/rZXKBTQtA==",
                    IsAdmin = true
                },
                new User {
                    Id = 2,
                    Username = "user",
                    Secret = "AQAAAAEAACcQAAAAELF/AS19WYRi8bYpl5oEULgnakHX2QJXtYEBDzDHir4XTZLjv9V4KEt0DplDqSpZ7A==",
                    IsAdmin = false
                });

            // https://github.com/aspnet/EntityFrameworkCore/issues/14051
            modelBuilder.Entity<Vote>().Property(e => e.Value).HasConversion(new BoolToZeroOneConverter<Int32>());
            modelBuilder.Entity<User>().Property(e => e.IsAdmin).HasConversion(new BoolToZeroOneConverter<Int32>());
            modelBuilder.Entity<Submission>().Property(e => e.IsWatched).HasConversion(new BoolToZeroOneConverter<Int32>());
        }
    }
}