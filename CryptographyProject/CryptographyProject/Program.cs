using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptographyProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = EncryptAes("123");
            var validA = a == EncryptAes("123");
            Console.WriteLine($"Password '123' result '{a}'. Validate 'EncryptAes(\"123\")' result {validA}.");
            var b = EncryptAes("AbC");
            var validB = b == EncryptAes("aBc");
            Console.WriteLine($"Password 'AbC' result '{b}'. Validate 'EncryptAes(\"aBc\")' result {validB}.");
            Console.Read();
        }

        #region Cryptography with AES
        private static string EncryptAes(string input)
        {
            return Convert.ToBase64String(EncryptAes(Encoding.UTF8.GetBytes(input)));
        }
        private static byte[] EncryptAes(byte[] input)
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes("7D21A773-FC11-4258-B024-D4ECFA0C980A", new byte[] { 0x43, 0x87, 0x23, 0x72, 0x45, 0x56, 0x68, 0x14, 0x62, 0x84 });
            MemoryStream ms = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = pdb.GetBytes(aes.KeySize / 8);
            aes.IV = pdb.GetBytes(aes.BlockSize / 8);
            CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(input, 0, input.Length);
            cs.Close();
            return ms.ToArray();
        }
        #endregion Cryptography with AES
    }
}
