using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace FGP_API.Models
{ 
    public class GameReview
    {
        [Key]
        public int ReviewId { get; set; }
        public Game ItemReviewed { get; set; }
        public int Rating { get; set; }

        [Required]
        [StringLength(200)]
        public string Comment { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDate { get; set; }
    }
}
