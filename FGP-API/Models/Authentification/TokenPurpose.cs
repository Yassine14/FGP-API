using System.ComponentModel.DataAnnotations;

namespace FGP_API.Models.Authentification
{
    public static class TokenPurpose
    {
        public const string FGPFront = "FGPFront";
        public const string FGPThirdParty = "FGPThirdParty"; 
    }
    public static class TokenName
    {
        public const string WebAuthentication = "WebAuthentication";
        public const string MobileAuthentication = "MobileAuthentication";
        public const string CloudAuthentication = "CloudAuthentication";

    }
}
