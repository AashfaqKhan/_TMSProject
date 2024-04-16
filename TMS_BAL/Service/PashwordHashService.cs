using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TMS_BAL.Service
{
    public class PashwordHashService
    {
        private readonly PasswordEncryptionService encryptionService;
        public PashwordHashService()
        {
            this.encryptionService = new PasswordEncryptionService();
        }
        public string GenerateSalt()
        {
            byte[] salt = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        
        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                
                byte[] combinedBytes = Encoding.UTF8.GetBytes(password );
                byte[] hashBytes = sha256.ComputeHash(combinedBytes);

                
                return Convert.ToBase64String(hashBytes);
            }
        }
        public string EncryptPassword(string hashedPassword)
        {
            
            return encryptionService.EncryptPassword(hashedPassword);
        }

        public string DecryptPassword(string encryptedPassword)
        {
            return encryptionService.DecryptPassword(encryptedPassword);
        }
    }
}
