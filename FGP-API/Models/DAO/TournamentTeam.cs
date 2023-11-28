using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace FGP_API.Models.DAO
{
    public class TournamentTeam
    { 
        [Key]
        public int TournamentId { get; set; }
        public Tournament? Tournament { get; set; } 
          
        [Key]
        public int TeamId { get; set; }
        public Team? Team { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDate { get; set; }
    }
}
