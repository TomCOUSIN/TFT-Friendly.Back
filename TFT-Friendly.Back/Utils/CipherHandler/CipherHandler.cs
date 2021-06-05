using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TFT_Friendly.Back.Utils.CipherHandler
{
    /// <summary>
    /// CipherHandler static class
    /// </summary>
    public static class CipherHandler
    {
        /// <summary>
        /// Decrypt the string given as parameter
        /// </summary>
        /// <param name="cipherText">The encrypted string to decrypt</param>
        /// <returns>The decrypted string</returns>
        public static string Decrypt(string cipherText)   
        {  
            const string encryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";  
            cipherText = cipherText.Replace(" ", "+");  
            var cipherBytes = Convert.FromBase64String(cipherText);

            using var encryptor = Aes.Create();
            var pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] {  
                0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76  
            });  
            encryptor.Key = pdb.GetBytes(32);  
            encryptor.IV = pdb.GetBytes(16);

            using var ms = new MemoryStream();
            using(var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write)) {  
                cs.Write(cipherBytes, 0, cipherBytes.Length);  
                cs.Close();  
            }  
            cipherText = Encoding.Unicode.GetString(ms.ToArray());
            
            return cipherText;  
        }
        
        /// <summary>
        /// Encrypt the string given as parameter
        /// </summary>
        /// <param name="encryptString">The string to encrypt</param>
        /// <returns>The encrypted string</returns>
        public static string Encrypt(string encryptString)   
        {  
            var encryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";  
            var clearBytes = Encoding.Unicode.GetBytes(encryptString);

            using Aes encryptor = Aes.Create();
            var pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] {  
                0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76  
            });  
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);

            using var ms = new MemoryStream();
            using(var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)) {  
                cs.Write(clearBytes, 0, clearBytes.Length);  
                cs.Close();  
            }  
            encryptString = Convert.ToBase64String(ms.ToArray());
            
            return encryptString;  
        } 
    }
}