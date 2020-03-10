using System;
using System.Security.Cryptography;
using System.Text;

namespace TBZ.HashingService
{
    public class MessageSalt
    {
        public string message { get; set; }
        public string salt { get; set; }

        public MessageSalt() { }
        public MessageSalt(string msg , string slt)
        {
            message = msg;
            salt = slt;
        }

        public string CreateSalt(int size)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public MessageSalt GenerateHash(MessageSalt msg)
        {
            if (msg.salt == null)
            {
                msg.salt = CreateSalt(32);
            }

            byte[] bytes = Encoding.UTF8.GetBytes(msg.message + msg.salt);
            SHA256Managed sHA256ManagedString = new SHA256Managed();
            byte[] hash = sHA256ManagedString.ComputeHash(bytes);
            msg.message = Convert.ToBase64String(hash);

            return msg;
        }
    }
}
