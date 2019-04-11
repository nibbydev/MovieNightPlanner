using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using DAL;
using DAL.Domain;

namespace NetGroupCV.Models {
    public class ListViewModel {
        public List<Submission> Submissions { get; set; }

        public ListViewModel(DbContext ctx) {
            Submissions = ctx.Submissions.Where(t => t.ImageUrl != null).ToList();
        }
    }
}