
using DAL.Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL {
    public partial class NgContext {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vote> Votes { get; set; }
    }
}