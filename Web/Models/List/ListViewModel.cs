using System.Collections.Generic;
using System.Linq;
using DAL;
using DAL.Domain;
using Microsoft.EntityFrameworkCore;

namespace MovieNight.Models.List {
    public class ListViewModel {
        public List<Submission> PlannedSubmissions { get; set; }
        public List<Submission> WatchedSubmissions { get; set; }
        public int Id { get; set; }
        public string Action { get; set; }

        public ListViewModel() { }

        public void GetPlannedSubmissions(MlContext ctx, string username) {
            PlannedSubmissions = ctx.Submissions
                .Where(t => !t.IsWatched)
                .Include(t => t.User)
                .Include(t => t.Votes)
                .ThenInclude(t => t.User)
                .ToList();

            // Count votes
            PlannedSubmissions.ForEach(t => t.UpVotes = t.Votes.Count(u => u.Value == 1));
            PlannedSubmissions.ForEach(t => t.DownVotes = t.Votes.Count(u => u.Value == -1));
            PlannedSubmissions.ForEach(t => t.Seen = t.Votes.Count(u => u.Value == 0));

            // Find submissions the current user has voted for
            if (username != null) {
                PlannedSubmissions.ForEach(t =>
                    t.UserHasVotedFor = t.Votes.Any(u => u.Value == 1 && u.User.Username.Equals(username)));
                PlannedSubmissions.ForEach(t =>
                    t.UserHasVotedAgainst = t.Votes.Any(u => u.Value == -1 && u.User.Username.Equals(username)));
                PlannedSubmissions.ForEach(t =>
                    t.UserHasSeen = t.Votes.Any(u => u.Value == 0 && u.User.Username.Equals(username)));
            }

            // Sort by total sum of votes
            PlannedSubmissions.Sort((j, i) => (i.UpVotes - i.DownVotes - i.Seen)
                .CompareTo(j.UpVotes - j.DownVotes - j.Seen));
        }

        public void GetWatchedSubmissions(MlContext ctx) {
            WatchedSubmissions = ctx.Submissions
                .Where(t => t.IsWatched)
                .Include(t => t.User)
                .ToList();
        }

        public bool DoAction(MlContext ctx, string username, out string msg) {
            switch (Action) {
                case "upvote":
                    return VoteSubmission(ctx, username, 1, out msg);
                case "downvote":
                    return VoteSubmission(ctx, username, -1, out msg);
                case "seen":
                    return VoteSubmission(ctx, username, 0, out msg);
                default:
                    msg = "Invalid action";
                    return false;
            }
        }

        private bool VoteSubmission(MlContext ctx, string username, int value, out string msg) {
            var userId = ctx.Users.First(t => t.Username.Equals(username)).Id;
            var vote = ctx.Votes.FirstOrDefault(t => t.UserId == userId && t.SubmissionId == Id);

            // Vote didn't exist
            if (vote == null) {
                vote = new Vote {
                    SubmissionId = Id,
                    Value = value,
                    UserId = userId
                };

                // Attempt to add to database
                try {
                    ctx.Votes.Add(vote);
                    ctx.SaveChanges();
                } catch {
                    msg = "An error occurred while adding the vote";
                    return false;
                }

                msg = "Successfully added vote";
                return true;
            }


            // Remove vote from database
            if (vote.Value == value) {
                try {
                    ctx.Votes.Remove(vote);
                    ctx.SaveChanges();
                    msg = "Successfully removed the vote";
                    return true;
                } catch {
                    msg = "An error occurred while adding the vote";
                    return false;
                }
            }

            // Attempt to update vote
            try {
                vote.Value = value;
                ctx.Votes.Update(vote);
                ctx.SaveChanges();
            } catch {
                msg = "An error occurred while adding the vote";
                return false;
            }

            msg = "Successfully updated the vote";
            return true;
        }
    }
}