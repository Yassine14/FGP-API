using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace FGP_API.Models.DAO
{
    public class Tournament
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public required string Name { get; set; }


        [StringLength(300)]
        public string? Description { get; set; }

        [StringLength(2000)]
        public string? Picture { get; set; }

        [ForeignKey("AdminId")]
        public int AdminId { get; set; }
        public AppUser? Admin { get; set; }
        public int? WiningTeamId { get; set; }
        public Team? WiningTeam { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDate { get; set; }  
        public ICollection<TournamentTeam>? Teams { get; set; } 
    }
}
