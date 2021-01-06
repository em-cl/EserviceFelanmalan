using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;

namespace Hv2020.Felanmalan.Login.WCF.Utils
{
    public class DataProcessing
    {
        /// <summary>
        /// whitelist med timeout för att förhindra regex denial of service
        /// https://docs.microsoft.com/en-us/dotnet/standard/base-types/how-to-strip-invalid-characters-from-a-string
        /// </summary>
        /// <param name="strInput"> input innan whitelist</param>
        /// <returns></returns>
        public string CleanInput(string strInput)
        {
            // Replace invalid characters with empty strings.
            try {
                return Regex.Replace(strInput, @"[^\w\.@-]", "",
                                     RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            // If we timeout when replacing invalid characters,
            // we should return Empty.
            catch (RegexMatchTimeoutException) {
                return String.Empty;
            }
        }
        /// <summary>
        /// Skapa salt
        /// https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-5.0
        /// </summary>
        /// <returns>salt string</returns>
        public string CreateSalt()
        {
            // generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create()) {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }
        /// <summary>
        /// Konvertera salt till byte
        /// https://stackoverflow.com/questions/6733845/c-sharp-convert-a-base64-byte
        /// kombinera och hasha salt och pass med sha 256
        /// https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-5.0
        /// </summary>
        /// <param name="salt"></param>
        /// <param name="password"></param>
        /// <returns>Salt+Password hash</returns>
        public string PasswordBuilder(string salt, string password)
        {
            byte[] decodedSalt = Convert.FromBase64String(salt);
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: decodedSalt,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}