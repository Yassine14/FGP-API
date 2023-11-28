using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FGP_API.Models.Enum
{
    public enum CancellationReasonEnum
    {
        WeatherConditions,
        VenueUnavailability,
        SafetyConcerns,
        RefereeDecision,
        LogisticalIssues,
        PlayerAbsence,
        ForceMajeure 
    }



}
