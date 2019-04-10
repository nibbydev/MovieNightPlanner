using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Domain {
    public class Movie {
        [Key]
        [Column(Order=1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        [MinLength(4),MaxLength(64)]
        public string UserName { get; set; }
        
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Time { get; set; }
        
        [Required]
        [MinLength(4),MaxLength(64)]
        public string Title { get; set; }
        
        [MinLength(4)]
        [MaxLength(256)]
        public string Image { get; set; }
        
        [Required]
        [MinLength(4),MaxLength(256)]
        public string Url { get; set; }
        
        public List<Tag> Tags { get; set; }
    }
}