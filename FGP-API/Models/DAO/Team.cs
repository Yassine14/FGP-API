﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace FGP_API.Models.DAO
{
    public class Team
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public required string Name { get; set; }
        public bool IsPublic { get; set; } = true;

        [StringLength(300)]
        public string? Description { get; set; }

        [StringLength(2000)]
        public string? Picture { get; set; }

        [ForeignKey("AdminId")]
        public int AdminId { get; set; }       
        public AppUser? Admin { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDate { get; set; }
        public ICollection<TeamPlayer> TeamPlayers { get; set; } = new List<TeamPlayer>();
        public ICollection<TeamMessage> Messages { get; set; } = new List<TeamMessage>();
        public ICollection<TeamRequest> UserRequests { get; set; } = new List<TeamRequest>();

        public ICollection<TournamentTeam>? Tournaments { get; set; }
    }
}
