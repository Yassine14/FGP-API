using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FGP_API.Models.DAO
{
    public class UserMessage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(300)]
        public required string Content { get; set; }
        public bool IsRead { get; set; } = false;

        [ForeignKey("Sender")]
        public int SenderId { get; set; }
        public AppUser? Sender { get; set; }

        [ForeignKey("Receiver")]
        public int ReceiverId { get; set; }
        public AppUser? Receiver { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDate { get; set; }
    }
}
