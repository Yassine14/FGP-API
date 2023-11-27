using FGP_API.Authentification;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FGP_API.Models
{
    public class FGPUser : IdentityUser
    {
        [Key]        
        public int Id { get; set; } 
         
        [StringLength(50)]
        public required string LastName { get; set; }

         
        [StringLength(50)]
        public required string FirstName { get; set; } 

        [StringLength(20)]
        public string? Nationality { get; set; }

        [StringLength(15)]
        public string? Gender { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string? Bio { get; set; }
        public string? FavouritePosition { get; set; } 
    
        [StringLength(50)]
        public  string? TimeZone { get; set; }

        [StringLength(2000)]
        public string? Picture { get; set; }
        public bool HideAttendingGames { get; set; } = false;
        public bool IsCompany { get; set; } = false;
        public bool IsSuspended { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public bool IsBlocked { get; set; } = false;
        public string? CompanyName { get; set; }

        [Precision(18, 2)]
        public decimal WalletBalance { get; set; } = 0;
        public int ReliabilityScore { get; set; }
        public UserNotification UserNotification { get; set; } 
        public ICollection<Notification> Notifications { get; set; }

        [InverseProperty("Creator")]
        public ICollection<Game> CreatedGames { get; set; }

        [InverseProperty("Host")]
        public ICollection<Game> HostedGames { get; set; }

        [InverseProperty("ManOfTheMatch")]
        public ICollection<Game> ManOfTheMatchs { get; set; }

        public ICollection<GameComment> Comments { get; set; }
        public ICollection<UserReview> UserReviews { get; set; } 
        public ICollection<Team> CreatedTeams { get; set; }  

        [InverseProperty("Sender")]
        public ICollection<UserMessage> SentMessages { get; set; }  

        [InverseProperty("Receiver")]
        public ICollection<UserMessage> ReceivedMessages { get; set; }  
    }
 

}
