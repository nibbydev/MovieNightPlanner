using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using DAL.Domain;

namespace NetGroupCV.Models {
    public class ListViewModel {
        public List<Submission> Submissions { get; set; }
        public int Id { get; set; }
        public bool Vote { get; set; }

        public ListViewModel() {
        }

        public void GetSubmissions(DbContext ctx) {
            Submissions = ctx.Submissions
                .Where(t => t.ImageUrl != null)
                .ToList();

            // Grab vote count for each submission
            foreach (var submission in Submissions) {
                var votes = ctx.Votes
                    .Where(t => t.SubmissionId == submission.Id)
                    .Select(t=>t.Value)
                    .ToList();

                submission.UpVotes = votes.Count(v => v);
                submission.DownVotes = votes.Count - submission.UpVotes;
            }
        }

        public bool AddVoteToDb(DbContext ctx) {
            var vote = new Vote {
                SubmissionId = Id,
                Value = Vote
            };
            
            // Attempt to add to database
            try {
                ctx.Votes.Add(vote);
                ctx.SaveChanges();
            } catch (Exception) {
                return false;
            }

            return true;
        }
    }
}