using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FGP_API.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Content { get; set; } 
        public bool IsSeen { get; set; }
        public int UserId { get; set; }
        public FGPUser User { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDate { get; set; }
    } 
}
