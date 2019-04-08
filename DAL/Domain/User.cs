using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Domain {
    public class User {
        [Key]
        [Column(Order=1)]
        public int Id { get; set; }
        
        [Required]
        [MinLength(4),MaxLength(64)]
        public string UserName { get; set; }
        
        [Required]
        [MinLength(4),MaxLength(4)]
        public string Pin { get; set; }
        
        [Required]
        public DateTime Joined { get; set; }
       
        [Required]
        public DateTime LastActive { get; set; }
    }
}