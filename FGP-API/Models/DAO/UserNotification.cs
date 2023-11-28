using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FGP_API.Models.DAO
{
    public class UserNotification
    {
        [Key]
        public int Id { get; set; }
        public bool NewFollower { get; set; } = true;
        public bool PrivateMsgReceived { get; set; } = true;
        public bool NewCommentGameIn { get; set; } = true;
        public bool GameAddedVenueFollowed { get; set; } = true;
        public bool GameHostUpdateAttendingList { get; set; } = true;
        public bool EmailNewPost { get; set; } = true;

        [ForeignKey("FGPUser")]
        public int FGPUserId { get; set; }
        public AppUser? FGPUser { get; set; }
    }
}
