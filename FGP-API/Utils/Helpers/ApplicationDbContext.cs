using FGP_API.Models.Authentification;
using FGP_API.Models.DAO; 
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FGP_API.Utils.Helpers
{
    public class ApplicationDbContext : IdentityDbContext<UserApplication>
    {
        private readonly ILoggerFactory? _loggerFactory;
        private readonly IConfiguration? _configuration;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ILoggerFactory loggerFactory, IConfiguration configuration)
        : base(options)
        {
            _loggerFactory = loggerFactory;
            _configuration = configuration;
        }

        public ApplicationDbContext()
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        // Enable database logging
        //        //optionsBuilder
        //        //    .UseSqlServer(_configuration.GetConnectionString("FGP-DEV-ConnectionString"))
        //        //    .UseLoggerFactory(_loggerFactory)
        //        //    .EnableSensitiveDataLogging();  
        //    }
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
           => optionsbuilder.ConfigureWarnings(c => c.Log((RelationalEventId.CommandExecuting, LogLevel.Information)));
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // This ensures EF Core stores the enum as string in the database 
            modelBuilder.Entity<Game>()
           .Property(m => m.CancellationMotif)
           .HasConversion<string>();

            modelBuilder.Entity<Game>()
             .HasOne(g => g.Creator)
             .WithMany(u => u.CreatedGames)
             .HasForeignKey(g => g.CreatorId)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Team>()
             .HasOne(g => g.Admin)
             .WithMany(u => u.CreatedTeams)
             .HasForeignKey(g => g.AdminId)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserMessage>()
            .HasOne(g => g.Sender)
            .WithMany(u => u.SentMessages)
            .HasForeignKey(g => g.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserMessage>()
             .HasOne(g => g.Receiver)
             .WithMany(u => u.ReceivedMessages)
             .HasForeignKey(g => g.ReceiverId)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tournament>()
             .HasOne(g => g.Admin)
             .WithMany(u => u.CreatedTournaments)
             .HasForeignKey(g => g.AdminId)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserReview>()
            .HasOne(g => g.Reviewed)
            .WithMany(u => u.UsersReviwedMe)
            .HasForeignKey(g => g.ReviewedUserId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserReview>()
           .HasOne(g => g.Reviewer)
           .WithMany(u => u.UserReviewedByMe)
           .HasForeignKey(g => g.ReviewerUserId)
           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TournamentTeam>()
            .HasKey(bc => new { bc.TournamentId, bc.TeamId });

            modelBuilder.Entity<TournamentTeam>()
                .HasOne(bc => bc.Tournament)
                .WithMany(b => b.Teams)
                .HasForeignKey(bc => bc.TournamentId);

            modelBuilder.Entity<TournamentTeam>()
                .HasOne(bc => bc.Team)
                .WithMany(c => c.Tournaments)
                .HasForeignKey(bc => bc.TeamId);


            modelBuilder.Entity<GamePlayerNonAppearance>()
            .HasKey(bc => new { bc.GameId, bc.UserId });

            modelBuilder.Entity<GamePlayerNonAppearance>()
                .HasOne(bc => bc.Game)
                .WithMany(b => b.NonAppearancesPlayers)
                .HasForeignKey(bc => bc.GameId);

            modelBuilder.Entity<GamePlayerNonAppearance>()
                .HasOne(bc => bc.User)
                .WithMany(c => c.NonAppearancesGames)
                .HasForeignKey(bc => bc.UserId);


            

            base.OnModelCreating(modelBuilder); 
        }
  
        public virtual DbSet<GameComment> GameComments { get; set; }
        public virtual DbSet<GameComment> TournamentsTeams { get; set; }
        public virtual DbSet<Tournament> Tournaments { get; set; } 
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<GameActivity> GameActivities { get; set; }
        public virtual DbSet<GameParticipant> GameParticipants { get; set; }
        public virtual DbSet<GamePlayerNonAppearance> NonAppearances { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Pitch> Pitchs { get; set; }
        public virtual DbSet<GameReview> GameReviews { get; set; }
        public virtual DbSet<UserReview> UserReviews { get; set; }
        public virtual DbSet<VenueReview> VenueReviews { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<TeamPlayer> TeamPlayers { get; set; }
        public virtual DbSet<TeamMessage> TeamMessages { get; set; }
        public virtual DbSet<TeamRequest> TeamRequests { get; set; }
        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<UserMessage> UserMessage { get; set; }
        public virtual DbSet<UserNotification> UserNotifications { get; set; }
        public virtual DbSet<Venue> Venues { get; set; }



    }
}
