using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FGP_API.Models
{
    public class GameParticipant
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Type { get; set; }  
        public bool IsPayementOk { get; set; }=false;

        
        public int? GameId { get; set; }
        public Game Game { get; set; }

        [Required]
        public int UserId { get; set; }
        public FGPUser User { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDate { get; set; }
    }
}
