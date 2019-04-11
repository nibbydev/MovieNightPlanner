using System;
using System.Collections.Generic;
using DAL.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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
            

            // https://github.com/aspnet/EntityFrameworkCore/issues/12278
            //modelBuilder.Entity<Vote>().Property(e => e.Value).HasColumnType("BIT(1)").HasConversion<int>();
            
            // https://github.com/aspnet/EntityFrameworkCore/issues/14051
            modelBuilder.Entity<Vote>().Property(e => e.Value).HasConversion(new BoolToZeroOneConverter<Int16>());
        }
    }
}