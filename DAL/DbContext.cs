
using DAL.Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL {
    public partial class DbContext {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}