using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Domain {
    public class Vote {
        [Key, Column(Order = 1), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Time { get; set; }

        public Submission Submission { get; set; }
        public int SubmissionId { get; set; }
        
        public User User { get; set; }
        public int UserId { get; set; }

        public bool Value { get; set; }
    }
}