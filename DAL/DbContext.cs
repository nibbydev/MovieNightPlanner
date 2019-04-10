
using DAL.Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL {
    public partial class DbContext {
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<Vote> Votes { get; set; }
    }
}