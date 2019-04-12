using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Domain {
    public class User {
        [Key, Column(Order = 1), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(Order = 2), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Joined { get; set; }

        [Required, MinLength(3), MaxLength(32)]
        public string Username { get; set; }

        [Required] public string Secret { get; set; }
        
        [Required] public bool IsAdmin { get; set; }
    }
}