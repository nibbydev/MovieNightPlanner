using System;
using System.Linq;
using DAL;

namespace MovieNight.Models {
    public class AdminViewModel {
        public int SubmissionDeleteId { get; set; }
        public int VoteResetId { get; set; }
        public string UserDeleteName { get; set; }

        public bool ResetVotes(MlContext ctx, out string msg) {
            if (VoteResetId == 0) {
                try {
                    ctx.Votes.RemoveRange(ctx.Votes);
                    ctx.SaveChanges();
                } catch (Exception e) {
                    msg = e.Message;
                    return false;
                }
            }

            var submission = ctx.Submissions.FirstOrDefault(t => t.Id.Equals(VoteResetId));
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

        public bool DeleteSubmission(MlContext ctx, out string msg) {
            var submission = ctx.Submissions.FirstOrDefault(t => t.Id.Equals(SubmissionDeleteId));
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

            msg = "User deleted";
            return true;
        }

        public bool DeleteUser(MlContext ctx, out string msg) {
            var user = ctx.Users.FirstOrDefault(t => t.Username.Equals(UserDeleteName));
            if (user == null) {
                msg = "No such user";
                return false;
            }

            try {
                ctx.Users.Remove(user);
                ctx.SaveChanges();
            } catch (Exception e) {
                msg = e.Message;
                return false;
            }

            msg = "User deleted";
            return true;
        }
    }
}