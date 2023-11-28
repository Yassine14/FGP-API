using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FGP_API.Models.DAO
{
    public class GameParticipant
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Type { get; set; } = string.Empty;
        public bool IsPayementOk { get; set; } = false;

        [Required]
        [ForeignKey("Game")]
        public int GameId { get; set; }
        public Game? Game { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public AppUser? User { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDate { get; set; }
    }
}
