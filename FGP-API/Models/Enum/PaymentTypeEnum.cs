using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FGP_API.Models.Enum
{


    public enum PaymentTypeEnum
    {
        Online,
        Cash,
        Free
    }

}
