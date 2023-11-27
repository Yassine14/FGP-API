using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 

namespace FGP_API.Models
{ 
    public class UserReview 
    {
        [Key]
        public int ReviewId { get; set; }

        public int FGPUserId { get; set; }
        public FGPUser FGPUser { get; set; }
        public int Rating { get; set; }
        [Required]
        [StringLength(200)]
        public string Comment { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDate { get; set; }
    }
}
