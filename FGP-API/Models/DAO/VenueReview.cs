using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace FGP_API.Models.DAO
{
    public class VenueReview
    {
        [Key]
        public int ReviewId { get; set; } 

        [ForeignKey("VenueReviewed")]
        public int VenueReviewedId { get; set; }
        public Venue? VenueReviewed { get; set; }

        [ForeignKey("VReviewer")]
        public int ReviewerUserId { get; set; }
        public AppUser? VenueReviewer { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDate { get; set; }
    }
}
