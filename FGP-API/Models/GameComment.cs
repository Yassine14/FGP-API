using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace FGP_API.Models
{ 
    public class GameComment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Content { get; set; }

        [Required]
        public DateTime PostedTime { get; set; }

         
        public int? GameId { get; set; }
        public Game Game { get; set; }

        [Required]
        public int UserId { get; set; }
        public FGPUser User { get; set; }

         

    }
}
