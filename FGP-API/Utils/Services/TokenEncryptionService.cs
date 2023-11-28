
using System.Text;
using System.Security.Cryptography;
using FGP_API.Utils.Helpers;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FGP_API.Utils.Services
{
    public class TokenEncryptionService(IConfiguration configuration)
    {
        private readonly IConfiguration _configuration = configuration;

        public string EncryptToken(string token)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(_configuration.GetRequiredSection("JWT:TokenEncryptionKey").Value ?? "");
            aes.IV = new byte[16]; // Initialization Vector - for simplicity, set to all zeros; in practice, generate a unique IV for each token

            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            byte[] encrypted;
            using (var ms = new System.IO.MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    byte[] tokenBytes = Encoding.UTF8.GetBytes(token);
                    cs.Write(tokenBytes, 0, tokenBytes.Length);
                }
                encrypted = ms.ToArray();
            }

            return Convert.ToBase64String(encrypted);
        }

        public string DecryptToken(string encryptedToken)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(_configuration["JWT:TokenEncryptionKey"] ?? "");
            aes.IV = new byte[16]; // Initialization Vector - for simplicity, set to all zeros; in practice, should match the IV used during encryption

            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            byte[] encryptedBytes = Convert.FromBase64String(encryptedToken);
            using var ms = new System.IO.MemoryStream(encryptedBytes);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var reader = new System.IO.StreamReader(cs);

            return reader.ReadToEnd();
        }


        /// <summary>
        /// Verify If Token is expired
        /// </summary> 
        /// <returns></returns>
        public bool IsExpired(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenObject = tokenHandler.ReadJwtToken(DecryptToken(token));
            if (tokenObject != null && tokenObject.ValidTo < DateTime.Now)
                return true;

            return false;
        }

        public string GenerateNewEncrytedToken(List<Claim> authClaims)
        {
            var issuerSigningKey = _configuration?["JWT:IssuerSigningKey"];
            var validIssuer = _configuration?["JWT:ValidIssuer"];
            var validAudience = _configuration?["JWT:ValidAudience"];

            var authSignInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(issuerSigningKey ?? ""));

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: validIssuer,
                audience: validAudience,
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSignInKey, SecurityAlgorithms.HmacSha256)
                );

            var Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return EncryptToken(Token);
        }

    }

}
