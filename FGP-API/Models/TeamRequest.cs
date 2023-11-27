using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace FGP_API.Models
{
    public class TeamRequest
    {
        [Key]
        public int Id { get; set; }
        public bool IsApproved { get; set; } = false;
        public DateTime DateApprovalDate { get; set; } 
        public int? RequesterId { get; set; }
        public FGPUser Requester { get; set; }
        public int? ApproverId { get; set; }
        public FGPUser Approver { get; set; } 
        public int? TeamId { get; set; }
        public Team Team { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDate { get; set; }

    }
}
