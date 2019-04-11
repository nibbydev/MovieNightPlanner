using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using DAL;
using DAL.Domain;
using MySql.Data.MySqlClient;

namespace NetGroupCV.Models {
    public class ListViewModel {
        private static readonly Regex UrlRegex = new Regex(@"^(https?://)?m\w{9}t\.net/\w{5}/\d+/.+$");
        private static readonly Regex NameRegex = new Regex(@"^[a-zA-Z_]+$");
        
        public List<Submission> Submissions { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }

        public ListViewModel() { }

        public ListViewModel(DbContext ctx) {
            Submissions = ctx.Submissions.Where(t => t.ImageUrl != null).ToList();
        }
        
        
        public bool Verify(out string msg) {
            if (string.IsNullOrEmpty(Url) || string.IsNullOrWhiteSpace(Url)) {
                msg = "Missing url";
                return false;
            }

            if (string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(Name)) {
                msg = "Missing name";
                return false;
            }

            if (Name.Length < 3) {
                msg = "Name must be longer than 3 characters";
                return false;
            }

            if (Name.Length > 64) {
                msg = "Name must not be longer than 64 characters";
                return false;
            }

            if (!NameRegex.IsMatch(Name)) {
                msg = "Name contains invalid characters";
                return false;
            }

            if (!UrlRegex.IsMatch(Url)) {
                msg = "Url does not match expected pattern";
                return false;
            }

            msg = "All good";
            return true;
        }

        public bool AddToDb(DbContext ctx) {
            var entry = Utility.ApiConnector.AsyncGet(37055).Result;
            
            var submission = new Submission {
                AddedBy = Name,
                Url = Url,
                MalId = entry.MalId,
                Title = entry.Title,
                Duration = entry.Duration,
                Episodes = entry.Episodes,
                ImageUrl = entry.ImageUrl,
                Score = entry.Score,
                Type = entry.Type,
                TrailerUrl = entry.TrailerUrl,
                Rating = entry.Rating,
                Synopsis = entry.Synopsis
            };

            try {
                ctx.Submissions.Add(submission);
                ctx.SaveChanges();
            } catch {
                return false;
            }

            return true;
        }
    }
}