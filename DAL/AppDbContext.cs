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
            
            modelBuilder.Entity<Movie>().HasData(
                new Movie {
                    Id = 1,
                    UserName = "catnib",
                    Time = DateTime.Now,
                    Title = "One Punch Man",
                    Url = "https://myanimelist.net/anime/30276/One_Punch_Man",
                    Image = "https://cdn.myanimelist.net/images/anime/12/76049.jpg"
                }, new Movie {
                    Id = 2,
                    UserName = "siegrest",
                    Time = DateTime.Now,
                    Title = "Mobile Suit Gundam Thunderbolt",
                    Url = "https://myanimelist.net/anime/31973/Mobile_Suit_Gundam_Thunderbolt",
                    Image = "https://cdn.myanimelist.net/images/anime/3/77176.jpg"
                }, new Movie {
                    Id = 3,
                    UserName = "rinnex",
                    Time = DateTime.Now,
                    Title = "Fairy Gone",
                    Url = "https://myanimelist.net/anime/39063/Fairy_Gone",
                    Image = "https://cdn.myanimelist.net/images/anime/1562/100460.jpg"
                });
            
            modelBuilder.Entity<Tag>().HasData(
                new Tag {
                    Id = 1,
                    Content = "action",
                    MovieId = 1
                }, new Tag {
                    Id = 2,
                    Content = "really cool",
                    MovieId = 1
                }, new Tag {
                    Id = 3,
                    Content = "military",
                    MovieId = 2
                }, new Tag {
                    Id = 4,
                    Content = "michael bay",
                    MovieId = 2
                }, new Tag {
                    Id = 5,
                    Content = "average at best",
                    MovieId = 2
                }, new Tag {
                    Id = 6,
                    Content = "eh",
                    MovieId = 3
                });

        }
    }
}