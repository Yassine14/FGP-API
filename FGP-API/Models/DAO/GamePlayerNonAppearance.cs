using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace FGP_API.Models.DAO
{
    public class GamePlayerNonAppearance
    {
        [Key] 
        public int UserId { get; set; }
        public AppUser? User { get; set; } 
        [Key]
        public int GameId { get; set; }
        public Game? Game { get; set; }

        [StringLength(300)]
        public required string Motif { get; set; }
    }
}
