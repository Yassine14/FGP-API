using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace FGP_API.Models
{
    public class NonAppearance
    {
        [Key]
        public int Id { get; set; } 
        public int UserId { get; set; }
        public FGPUser User { get; set; }
        public int? GameId { get; set; }
        public Game Game { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDate { get; set; }
    }
}
