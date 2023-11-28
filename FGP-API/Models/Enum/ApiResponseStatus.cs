using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FGP_API.Models.Enum
{


    public enum ApiResponseStatus
    {
        [Description("Success")]
        Success,
        [Description("Error")]
        Error,
        [Description("Unauthorized")]
        Unauthorized,
        [Description("NotFound")]
        NotFound,
        [Description("Conflict")]
        Conflict
    }

}
