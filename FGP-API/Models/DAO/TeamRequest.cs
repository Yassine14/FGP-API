using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace FGP_API.Models.DAO
{
    public class TeamRequest
    {
        [Key]
        public int Id { get; set; }
        public bool IsApproved { get; set; } = false;
        public DateTime? ApprovalDate { get; set; }

        [ForeignKey("RequesterId")]
        public int RequesterId { get; set; } 
        public AppUser? Requester { get; set; }

        [ForeignKey("ApproverId")]
        public int? ApproverId { get; set; } 
        public AppUser? Approver { get; set; }

        [ForeignKey("TeamId")]
        public int TeamId { get; set; } 
        public Team? Team { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDate { get; set; }

    }
}
