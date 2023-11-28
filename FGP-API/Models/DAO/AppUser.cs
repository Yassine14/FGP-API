using FGP_API.Models.Authentification;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FGP_API.Models.DAO
{
    public class AppUser  
    {
        [Key] 
        public int Id { get; set; }

        [StringLength(50)]
        public required string LastName { get; set; } 
       
        [StringLength(50)]
        public required string FirstName { get; set; }
        [StringLength(50)]
        public required string UserName { get; set; }
        [StringLength(150)]
        public required string Email { get; set; }
        [StringLength(12)]
        public required string PhoneNumber { get; set; }
 
        [StringLength(20)]
        public string? Nationality { get; set; }

        [StringLength(15)]
        public string? Gender { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string? Bio { get; set; }
        public string? FavouritePosition { get; set; }

        [StringLength(50)]
        public string? TimeZone { get; set; }

        [StringLength(2000)]
        public string? Picture { get; set; }
        public bool HideAttendingGames { get; set; } = false;
        public bool IsCompany { get; set; } = false;
        public bool IsPremium { get; set; } = false;
        public bool IsSuspended { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public bool IsBlocked { get; set; } = false;
        public string? CompanyName { get; set; }

        [Precision(18, 2)]
        public decimal WalletBalance { get; set; } = 0;
        public int ReliabilityScore { get; set; }
        public UserNotification? UserNotification { get; set; }  
        public ICollection<Notification>? Notifications { get; set; }  

        [InverseProperty("Creator")]
        public ICollection<Game>? CreatedGames { get; set; }

        [InverseProperty("Host")]
        public ICollection<Game>? HostedGames { get; set; }

        [InverseProperty("ManOfTheMatch")]
        public ICollection<Game>? ManOfTheMatchs { get; set; }

        public ICollection<GameComment>? Comments { get; set; }

        [InverseProperty("Reviewer")]
        public ICollection<UserReview>? UserReviewedByMe { get; set; }
        [InverseProperty("Reviewed")]
        public ICollection<UserReview>? UsersReviwedMe { get; set; }
        public ICollection<GameReview>? MyGameReviews { get; set; }
        public ICollection<VenueReview>? MyVenueReviews { get; set; }

       

        public ICollection<Team>? CreatedTeams { get; set; }
        public ICollection<Tournament>? CreatedTournaments { get; set; }


        [InverseProperty("Sender")]
        public ICollection<UserMessage>? SentMessages { get; set; }

        [InverseProperty("Receiver")]
        public ICollection<UserMessage>? ReceivedMessages { get; set; }
        public ICollection<GamePlayerNonAppearance>? NonAppearancesGames { get; set; }
        public string UserApplicationId { get; set; } = string.Empty;

    }


}
