using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace TMS_BAL.Service
{
    public class PasswordEncryptionService
    {
        private readonly string encryptionKey = "ThisIsAStrongEncryptionKey1234";

        private readonly byte[] iv;

        public PasswordEncryptionService()
        {
            iv = GenerateRandomIV();
        }

        public string EncryptPassword(string hashedPassword)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = DeriveKeyBytes(encryptionKey);
                aesAlg.IV = iv;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(hashedPassword);
                        }
                    }
                    return Convert.ToBase64String(iv.Concat(msEncrypt.ToArray()).ToArray());
                }
            }
        }

        public string DecryptPassword(string encryptedPassword)
        {
            if (string.IsNullOrEmpty(encryptedPassword) || encryptedPassword.Length < 16)
            {
                throw new ArgumentException("Invalid encrypted password. The length of the encrypted password must be at least 16 characters.");
            }

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = DeriveKeyBytes(encryptionKey);
                aesAlg.IV = iv;

                int ivLength = 16;

                if (encryptedPassword.Length < ivLength)
                {
                    throw new ArgumentException("Invalid encrypted password. The IV is missing or incomplete.");
                }

                aesAlg.IV = encryptedPassword.Substring(0, ivLength).Select(c => (byte)c).ToArray();

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                try
                {
                    using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(encryptedPassword.Substring(ivLength))))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                return srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
                catch (Exception)
                {
                   
                    throw;
                }
            }
        }

        private byte[] GenerateRandomIV()
        {
            byte[] iv = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(iv);
            }
            return iv;
        }
        private byte[] DeriveKeyBytes(string key)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(key, iv, 10000)) // Use IV for salt
            {
                return deriveBytes.GetBytes(32); // 256 bits for AES
            }
        }
    }
}
