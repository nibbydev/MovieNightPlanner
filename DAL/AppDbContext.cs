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
                    Secret = null,    // Pass will be set on first login
                    IsAdmin = true
                });

            // https://github.com/aspnet/EntityFrameworkCore/issues/14051
            modelBuilder.Entity<User>().Property(e => e.IsAdmin).HasConversion(new BoolToZeroOneConverter<int>());
            modelBuilder.Entity<Submission>().Property(e => e.IsWatched).HasConversion(new BoolToZeroOneConverter<int>());
        }
    }
}