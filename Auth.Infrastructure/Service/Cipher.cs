using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Auth.Infrastructure.Service
{
    public class Cipher
    {
        public static IConfigurationRoot Configuration;
        private static string keyString;
        public Cipher()
        {
            if (keyString != string.Empty) {
                var builder = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json");
                Configuration = builder.Build();
                keyString = Configuration["Cipher_Key:Key"];
            }

        }
        public string Encrypt(string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            try {
                var key = Encoding.UTF8.GetBytes(keyString);

                using (var aesAlg = Aes.Create()) {
                    using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV)) {
                        using (var msEncrypt = new MemoryStream()) {
                            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                            using (var swEncrypt = new StreamWriter(csEncrypt)) {
                                swEncrypt.Write(value);
                            }

                            var iv = aesAlg.IV;

                            var decryptedContent = msEncrypt.ToArray();

                            var result = new byte[iv.Length + decryptedContent.Length];

                            Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                            Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                            var str = Convert.ToBase64String(result);
                            var fullCipher = Convert.FromBase64String(str);
                            return str;
                        }
                    }
                }
            }
            catch (Exception ex) {
                return ex.Message;
            }
        }


        public string Decrypt(string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            try {
                value = value.Replace(" ", "+");
                var fullCipher = Convert.FromBase64String(value);

                var iv = new byte[16];
                var cipher = new byte[fullCipher.Length - iv.Length];

                Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
                Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, fullCipher.Length - iv.Length);
                var key = Encoding.UTF8.GetBytes(keyString);

                using (var aesAlg = Aes.Create()) {
                    using (var decryptor = aesAlg.CreateDecryptor(key, iv)) {
                        string result;
                        using (var msDecrypt = new MemoryStream(cipher)) {
                            using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)) {
                                using (var srDecrypt = new StreamReader(csDecrypt)) {
                                    result = srDecrypt.ReadToEnd();
                                }
                            }
                        }
                        return result;
                    }
                }
            }
            catch (Exception ex) {
                return ex.Message;
            }
        }
    }
}
