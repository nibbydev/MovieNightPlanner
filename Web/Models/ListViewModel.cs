using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DAL;
using DAL.Domain;

namespace NetGroupCV.Models {
    public class ListViewModel {
        private static readonly Regex UrlRegex = new Regex(@"^(https?://)?m\w{9}t\.net/\w{5}/\d+/.+$");
        private static readonly Regex NameRegex = new Regex(@"^[a-zA-Z_]+$");

        public List<Submission> Submissions { get; set; }

        public string PostUrl { get; set; }
        public string PostName { get; set; }

        public ListViewModel() { }

        public ListViewModel(DbContext ctx) {
            Submissions = ctx.Submissions.Where(t => t.ImageUrl != null).ToList();
        }

        public bool VerifySubmission(out string msg) {
            if (string.IsNullOrEmpty(PostUrl) || string.IsNullOrWhiteSpace(PostUrl)) {
                msg = "Missing url";
                return false;
            }

            if (string.IsNullOrEmpty(PostName) || string.IsNullOrWhiteSpace(PostName)) {
                msg = "Missing name";
                return false;
            }

            if (PostName.Length < 3) {
                msg = "Name must be longer than 3 characters";
                return false;
            }

            if (PostName.Length > 64) {
                msg = "Name must not be longer than 64 characters";
                return false;
            }

            if (!NameRegex.IsMatch(PostName)) {
                msg = "Name contains invalid characters";
                return false;
            }

            if (!UrlRegex.IsMatch(PostUrl)) {
                msg = "Url does not match expected pattern";
                return false;
            }

            msg = "All good";
            return true;
        }

        public void AddSubmission(DbContext ctx) {
            ctx.Submissions.Add(new Submission {
                AddedBy = PostName,
                Url = PostUrl
            });

            ctx.SaveChanges();
        }
    }
}