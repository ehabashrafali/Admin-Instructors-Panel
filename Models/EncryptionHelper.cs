
using System.Security.Cryptography;
using System.Text;


namespace Admin_Panel_ITI.Models
{

    public static class EncryptionHelper
    {
        //generate key
        public static string GenerateRandomKey()
        {
            return new Guid().ToString();
        }

        //generate IV
        public static string GenerateRandomIV()
        {
            return new Guid().ToString();
        }

        private static readonly string Key = GenerateRandomKey();
        private static readonly string IV = GenerateRandomIV();

        public static string Encrypt(string plainText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.KeySize = 128; 

                aesAlg.Key = Convert.FromBase64String(Key);
                aesAlg.IV = Convert.FromBase64String(IV);

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }

                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public static string Decrypt(string cipherText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.KeySize = 128; 

                aesAlg.Key = Convert.FromBase64String(Key); // Convert the Base64-encoded key back to byte[]
                aesAlg.IV = Convert.FromBase64String(IV);

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                var x = Convert.FromBase64String(cipherText);

                using (MemoryStream msDecrypt = new(x))
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
        }
    }

}
