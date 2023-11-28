using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace FGP_API.Models.DAO
{
    public class GameComment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Content { get; set; } = string.Empty;

        [Required]
        public DateTime PostedTime { get; set; }

        
        [Required]
        [ForeignKey("Game")]
        public int GameId { get; set; }
        public Game? Game { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public AppUser? User { get; set; }



    }
}
