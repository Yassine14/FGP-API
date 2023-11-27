using FGP_API.Authentification;
using FGP_API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace FGP_API.Utils.Helpers
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected ApplicationDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
        }

        public virtual DbSet<GameComment> GameComments { get; set; } 
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<GameActivity> GameActivities { get; set; }
        public virtual DbSet<GameParticipant> GameParticipants { get; set; }
        public virtual DbSet<NonAppearance> NonAppearances { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Pitch> Pitchs { get; set; }
        public virtual DbSet<GameReview> GameReviews { get; set; }
        public virtual DbSet<UserReview> UserReviews { get; set; }
        public virtual DbSet<VenueReview> VenueReviews { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<TeamPlayer> TeamPlayers { get; set; }
        public virtual DbSet<TeamMessage> TeamMessages { get; set; }
        public virtual DbSet<TeamRequest> TeamRequests { get; set; }
        public virtual DbSet<FGPUser> FGPUsers { get; set; }
        public virtual DbSet<UserMessage> UserMessage { get; set; }
        public virtual DbSet<UserNotification> UserNotifications { get; set; }
        public virtual DbSet<Venue> Venues { get; set; }



    }
}
