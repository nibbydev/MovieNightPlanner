using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Domain {
    public class Tag {
        [Key]
        [Column(Order=1)]
        public int Id { get; set; }
        
        [Required]
        [MinLength(1), MaxLength(64)]
        public string Content { get; set; }
        
        [Required]
        public Movie Movie { get; set; }
        public int MovieId { get; set; }
    }
}