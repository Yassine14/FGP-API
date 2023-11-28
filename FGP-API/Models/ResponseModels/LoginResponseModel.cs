using FGP_API.Models.DAO;
using FGP_API.Utils.Services;

namespace FGP_API.Models.ResponseModels
{
    public class LoginResponseModel : Response
    { 
        public AuthentificationData? Data { get; set; } = null;
        public string[]? Errors { get; set; }
    }

    public class AuthentificationData(TokenEncryptionService tokenEncryptionService)
    {
        private readonly TokenEncryptionService _tokenEncryptionService = tokenEncryptionService;
        private string? _token;
        public string EncryptedToken
        {
            get { return _token ?? ""; }
            set
            {
                // Call the encryption function before setting the value
                _token = _tokenEncryptionService.EncryptToken(value);
            }
        }
        public DateTime? Expires { get; set; }

    }
}
