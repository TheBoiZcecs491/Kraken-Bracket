using System;
using System.Security.Cryptography;
using System.Text;

namespace TBZ.HashingService
{
    public class MessageSalt
    {
        public string message;
        public string salt;

        public MessageSalt(string msg, string slt = null)
        {
            this.message = msg;
            this.salt = slt;
        }
    }

    public class Hashing
    {
        public string CreateSalt(int size)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public MessageSalt GenerateHash(MessageSalt obj)
        {
            if(obj.salt == null)
            {
                obj.salt = CreateSalt(32);
            }

            byte[] bytes = Encoding.UTF8.GetBytes(obj.message + obj.salt);
            SHA256Managed sHA256ManagedString = new SHA256Managed();
            byte[] hash = sHA256ManagedString.ComputeHash(bytes);
            obj.message = Convert.ToBase64String(hash);

            return obj;
        }
    }
}
