using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FGP_API.Models
{


    public class Game
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public required string Title { get; set; }

        [Required]
        public int VenueId { get; set; }
        public required Venue Venue { get; set; }

        [StringLength(3000)]
        public string?  Description { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public TimeOnly StartTime { get; set; }
        public int DurationMin { get; set; }
        public bool AcceptsDifferentGenders { get; set; }
        [Required]
        [StringLength(20)]
        public string? Gender { get; set; }
        [Required]
        public bool IsPrivate { get; set; }
        [Required]
        public int MaxPlayersPerTeam { get; set; }
        public bool AcceptMatchOrganizer { get; set; } = false;
        public bool IsTrainingSession { get; set; } = false;
        public bool IsIndoor { get; set; } = false;

        [Required]
        [StringLength(20)]
        public required string TypePayment { get; set; }
        public bool IsPaid { get; set; }

        [Precision(18, 2)]
        public decimal FeePerUser { get; set; }
        public bool RefundOnCancellation { get; set; } = false;
        public bool RefundOnEditRSVP { get; set; } = false;
        public int RefundDaysBeforeMatch { get; set; }
        public bool IsCancelled { get; set; } = false;

        [StringLength(500)]
        public string? CancellationMotif { get; set; }
        public DateTime CancellationDate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdateDate { get; set; }

        [ForeignKey("Creator")]
        public int CreatorId { get; set; }
        public FGPUser Creator { get; set; }

        [ForeignKey("Host")]
        public int? HostId { get; set; }
        public FGPUser Host { get; set; }

        [ForeignKey("ManOfTheMatch")]
        public int? ManOfTheMatchId { get; set; }
        public FGPUser ManOfTheMatch { get; set; }
        public ICollection<string> Keywords { get; set; } = new List<string>(); 
        
        public ICollection<GameActivity> GameActivities { get; set; } = new List<GameActivity>();
        public ICollection<GameParticipant> Participants { get; set; } = new List<GameParticipant>();

        public ICollection<GameComment> Comments { get; set; } = new List<GameComment>();
        public ICollection<NonAppearance> NonAppearances { get; set; } = new List<NonAppearance>();
        public ICollection<GameReview> Reviews { get; set; } = new List<GameReview>();
    }

}
