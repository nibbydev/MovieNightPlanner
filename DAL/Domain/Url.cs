using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Domain {
    public class Url {
        [Key]
        [Column(Order=1)]
        public int Id { get; set; }
        
        [Required]
        [MinLength(4),MaxLength(256)]
        public string Link { get; set; }
        
        [MinLength(4),MaxLength(64)]
        public string Title { get; set; }
        
        [MaxLength(256)]
        public string Description { get; set; }
    }
}