
using DAL.Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL {
    public partial class MlContext {
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<User> Users { get; set; }
    }
}