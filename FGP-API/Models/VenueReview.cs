using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace FGP_API.Models
{ 
    public class VenueReview 
    {
        [Key]
        public int ReviewId { get; set; }
        public Venue ItemReviewed { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDate { get; set; }
    }
}
