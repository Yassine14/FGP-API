using FGP_API.Models.DAO;

namespace FGP_API.Models.ResponseModels
{
    public class RegistrationResponseModel : Response
    { 
        public string? Username { get; set; }
        public string[]? Errors { get; set; }
    }
}
