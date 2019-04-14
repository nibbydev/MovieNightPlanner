using System;
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
        public bool Vote { get; set; }

        public ListViewModel() { }


        public void GetPlannedSubmissions(MlContext ctx, string username) {
            PlannedSubmissions = ctx.Submissions
                .Where(t => !t.IsWatched)
                .Include(t => t.User)
                .Include(t => t.Votes)
                .ThenInclude(t => t.User)
                .ToList();

            // Count votes
            PlannedSubmissions.ForEach(t => t.UpVotes = t.Votes.Count(u => u.Value));
            PlannedSubmissions.ForEach(t => t.DownVotes = t.Votes.Count(u => !u.Value));

            // Find submissions the current user has voted for
            if (username != null) {
                PlannedSubmissions.ForEach(t =>
                    t.UserHasVotedFor = t.Votes.Any(u => u.Value && u.User.Username.Equals(username)));
                PlannedSubmissions.ForEach(t =>
                    t.UserHasVotedAgainst = t.Votes.Any(u => !u.Value && u.User.Username.Equals(username)));
            }

            // Sort by total sum of votes
            PlannedSubmissions.Sort((j, i) => (i.UpVotes - i.DownVotes).CompareTo(j.UpVotes - j.DownVotes));
        }

        public void GetWatchedSubmissions(MlContext ctx, string username) {
            WatchedSubmissions = ctx.Submissions
                .Where(t => t.IsWatched)
                .ToList();
        }

        public bool AddVoteToDb(MlContext ctx, string username) {
            var userId = ctx.Users.First(t => t.Username.Equals(username)).Id;
            var vote = ctx.Votes.FirstOrDefault(t => t.UserId == userId && t.SubmissionId == Id);

            if (vote != null) {
                // Remove vote
                if (vote.Value == Vote) {
                    try {
                        ctx.Votes.Remove(vote);
                        ctx.SaveChanges();
                        return true;
                    } catch (Exception) {
                        return false;
                    }
                }

                // Attempt to update vote
                try {
                    vote.Value = Vote;
                    ctx.Votes.Update(vote);
                    ctx.SaveChanges();
                    return true;
                } catch (Exception) {
                    return false;
                }
            }

            vote = new Vote {
                SubmissionId = Id,
                Value = Vote,
                UserId = ctx.Users.First(t => t.Username.Equals(username)).Id
            };

            // Attempt to add to database
            try {
                ctx.Votes.Add(vote);
                ctx.SaveChanges();
                return true;
            } catch (Exception) {
                return false;
            }
        }
    }
}