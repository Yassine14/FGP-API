﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FGP_API.Models
{ 
    public class Pitch  
    {
        [Key]
        public int Id { get; set; }
        
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        public int MaxPlayersPerTeam { get; set; }

        [Required]
        public int VenueId { get; set; }  
        public Venue Venue { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDate { get; set; }

    }

}
