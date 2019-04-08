using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Domain {
    public class Movie {
        [Key]
        [Column(Order=1)]
        public int Id { get; set; }
        
        [Required]
        public User AddedBy { get; set; }
        
        [Required]
        public DateTime Time { get; set; }
        
        [Required]
        [MinLength(4),MaxLength(64)]
        public string Title { get; set; }
        
        [MinLength(4),MaxLength(256)]
        public string Description { get; set; }
        
        [MinLength(4)]
        [MaxLength(256)]
        public string Image { get; set; }
        
        public List<Url> Sources { get; set; }
    }
}