
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Domain {
    public class Vote {
        [Key]
        [Column(Order=1)]
        public int Id { get; set; }
        
        [Required]
        public short Value { get; set; }
        
        [Required]
        public DateTime Time { get; set; }
        
        [Required]
        public User User { get; set; }
    }
}