using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FGP_API.Models.DAO
{
    public class UserReview
    {
        [Key]
        public int ReviewId { get; set; }

        [ForeignKey("Reviewed")]
        public int ReviewedUserId { get; set; }
        public AppUser? Reviewed { get; set; }

        [ForeignKey("Reviewer")]
        public int ReviewerUserId { get; set; }
        public AppUser? Reviewer { get; set; }
        public int Rating { get; set; }
        [Required]
        [StringLength(200)]
        public string Comment { get; set; } = string.Empty;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDate { get; set; }
    }
}
