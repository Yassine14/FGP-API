using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FGP_API.Models.DAO
{
    public class GameActivity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public required string ActivityType { get; set; }

        [StringLength(200)]
        public string? Libelle { get; set; }

        [Required]
        public int GameId { get; set; }
        public Game? Game { get; set; }

        [Required]
        public int UserId { get; set; }
        public AppUser? User { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDate { get; set; }
    }
}
