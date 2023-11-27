﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FGP_API.Models
{
    public class Venue  
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string MapPosition { get; set; }

        [StringLength(3000)]
        public string? Description { get; set; }

        [StringLength(2000)]
        public string? Picture { get; set; }
        [Required]
        [StringLength(100)]
        public string Adresse { get; set; }
        [StringLength(100)]
        public string? Adresse2 { get; set; }
        [StringLength(15)]
        public string? PhoneNumber { get; set; }
        [Required]
        [StringLength(100)]
        public required string City { get; set; }
        [Required]
        [StringLength(7)]
        public required string PostalCode { get; set; }
        [Required]
        [StringLength(100)]
        public required string Country { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDate { get; set; }

        public ICollection<Pitch> Pitchs { get; set; } = new List<Pitch>();
        public ICollection<Game> Games { get; set; } = new List<Game>();
        public ICollection<VenueReview> Reviews { get; set; } = new List<VenueReview>(); 
    }
}
