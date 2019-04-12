using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using DAL.Domain;
using Microsoft.EntityFrameworkCore;

namespace MovieNight.Models.Admin {
    public class AdminSubmissionsViewModel {
        public List<Submission> Submissions { get; set; }
        public string Action { get; set; }
        public int Id { get; set; }

        public AdminSubmissionsViewModel() { }

        public void GetSubmissions(MlContext ctx) {
            Submissions = ctx.Submissions
                .Include(t => t.User)
                .ToList();
        }

        public bool DoAction(MlContext ctx, out string msg) {
            switch (Action) {
                case "delete":
                    return DeleteSubmission(ctx, out msg);
                case "reset":
                    return ResetVotes(ctx, out msg);
                case "watched":
                    return ToggleWatched(ctx, out msg);
                default:
                    msg = "Invalid action";
                    return false;
            }
        }

        private bool DeleteSubmission(MlContext ctx, out string msg) {
            var submission = ctx.Submissions.FirstOrDefault(t => t.Id.Equals(Id));
            if (submission == null) {
                msg = "No such submission";
                return false;
            }

            try {
                ctx.Submissions.Remove(submission);
                ctx.SaveChanges();
            } catch (Exception e) {
                msg = e.Message;
                return false;
            }

            msg = "Submission deleted";
            return true;
        }

        private bool ResetVotes(MlContext ctx, out string msg) {
            var submission = ctx.Submissions.FirstOrDefault(t => t.Id.Equals(Id));
            if (submission == null) {
                msg = "No such submission";
                return false;
            }

            try {
                ctx.Votes.RemoveRange(ctx.Votes.Where(t => t.SubmissionId.Equals(submission.Id)));
                ctx.SaveChanges();
            } catch (Exception e) {
                msg = e.Message;
                return false;
            }

            msg = "Successfully reset votes";
            return true;
        }

        private bool ToggleWatched(MlContext ctx, out string msg) {
            var submission = ctx.Submissions.FirstOrDefault(t => t.Id.Equals(Id));
            if (submission == null) {
                msg = "No such submission";
                return false;
            }

            submission.IsWatched = !submission.IsWatched;

            try {
                ctx.Submissions.Update(submission);
                ctx.SaveChanges();
            } catch (Exception e) {
                msg = e.Message;
                return false;
            }

            msg = "Successfully toggled watched state";
            return true;
        }
    }
}