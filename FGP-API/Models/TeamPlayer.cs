using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FGP_API.Models
{
    public class TeamPlayer
    {
        [Key]
        public int Id { get; set; } 
         
        [StringLength(50)]
        public string? Position { get; set; }  
        public int? TeamId { get; set; }
        public Team Game { get; set; } 
        public int UserId { get; set; }
        public FGPUser User { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDate { get; set; }
    }
}
