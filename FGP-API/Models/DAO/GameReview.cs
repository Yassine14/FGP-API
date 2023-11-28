using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FGP_API.Models.DAO
{
    public class GameReview
    {
        [Key]
        public int ReviewId { get; set; }

        [ForeignKey("GameReviewed")]
        public int GameReviewedId { get; set; }
        public Game? GameReviewed { get; set; }

        [ForeignKey("GReviewer")]
        public int ReviewerUserId { get; set; }
        public AppUser? GameReviewer { get; set; }
        public int Rating { get; set; }

        [Required]
        [StringLength(200)]
        public string Comment { get; set; } = string.Empty;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDate { get; set; }
    }
}
