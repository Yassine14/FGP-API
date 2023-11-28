using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FGP_API.Models.DAO
{
    public class TeamPlayer
    {
        [Key]
        public int Id { get; set; } 
        [ForeignKey("Game")]
        public int TeamId { get; set; }
        public Team? Team { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public AppUser? User { get; set; }

        [StringLength(50)]
        public string? Position { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDate { get; set; }
    }
}
