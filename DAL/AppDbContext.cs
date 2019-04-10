using System;
using System.Collections.Generic;
using DAL.Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL {
    public partial class DbContext : Microsoft.EntityFrameworkCore.DbContext {
        public DbContext() { }

        public DbContext(DbContextOptions<DbContext> options)
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
            
            modelBuilder.Entity<Submission>().HasIndex(e=>e.MalId).IsUnique();
            
            modelBuilder.Entity<Submission>().HasData(
                new Submission {
                    Id = 1,
                    AddedBy = "catnib",
                    Time = DateTime.Now,
                    Title = "One Punch Man",
                    Url = "https://myanimelist.net/anime/30276/One_Punch_Man",
                    ImageUrl = "https://cdn.myanimelist.net/images/anime/12/76049.jpg"
                }, new Submission {
                    Id = 2,
                    AddedBy = "siegrest",
                    Time = DateTime.Now,
                    Title = "Mobile Suit Gundam Thunderbolt",
                    Url = "https://myanimelist.net/anime/31973/Mobile_Suit_Gundam_Thunderbolt",
                    ImageUrl = "https://cdn.myanimelist.net/images/anime/3/77176.jpg"
                }, new Submission {
                    Id = 3,
                    AddedBy = "rinnex",
                    Time = DateTime.Now,
                    Title = "Fairy Gone",
                    Url = "https://myanimelist.net/anime/39063/Fairy_Gone",
                    ImageUrl = "https://cdn.myanimelist.net/images/anime/1562/100460.jpg"
                });
        }
    }
}