using System;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;

namespace FGP_API.Utils.Services
{ 
    public class TokenEncryptionService
    {
        private readonly IConfiguration _configuration; 
        public TokenEncryptionService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public string EncryptToken(string token)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(_configuration.GetRequiredSection("JWT:TokenEncryptionKey").Value);
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
            aes.Key = Encoding.UTF8.GetBytes(_configuration["JWT:TokenEncryptionKey"]);
            aes.IV = new byte[16]; // Initialization Vector - for simplicity, set to all zeros; in practice, should match the IV used during encryption

            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            byte[] encryptedBytes = Convert.FromBase64String(encryptedToken);
            using var ms = new System.IO.MemoryStream(encryptedBytes);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var reader = new System.IO.StreamReader(cs);

            return reader.ReadToEnd();
        }

        //private bool HasValidTokenForUser(string userId, string purpose, int tokenValidityDurationMinutes)
        //{
        //    string existingToken = GetTokenForUser(userId, purpose);

        //    // If no token found for the user, or it's expired, return false
        //    if (string.IsNullOrEmpty(existingToken))
        //    {
        //        return false;
        //    }

        //    // Check token expiration
        //    //DateTime tokenCreatedAt = /* Get the creation time of the token from your storage */
        //    //DateTime expirationTime = tokenCreatedAt.AddHours(tokenValidityDurationMinutes);

        //    // If the current time is before the expiration time, the token is considered valid
             
        //}

        //public string GetTokenForUser(string userId, string purpose)
        //{
        //    // Code to retrieve the token for the specified user and purpose from your storage
        //    // Return the token or null if not found or expired

        //    var token = _userManager.toke
        //    .FirstOrDefault(t => t.UserId == userId && t.Name == purpose);

        //    return token?.Value;
        //}
    }

}
