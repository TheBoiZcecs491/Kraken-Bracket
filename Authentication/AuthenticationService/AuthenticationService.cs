﻿using Data.AccessLayer;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Authentication.Services
{
    public class AuthenticationService
    {
        private const string _algorithm = "HmacSHA256";
        private const string _salt = "rz8LuOtFBXphj9WQfvFh";

        public string AuthenticateUser(string email, string password)
        {
            var dataAccess = new DataAccess();
            bool found = dataAccess.GetEmailAndPassword(email, password);
            string claim;
            string token;
            if (found == true)
            {
                claim = GetClaim(email);
                token = GenerateToken(email, password, claim);
                return token;
            }
            else
            {
                throw new ArgumentException("No results returned");
            }
        }

        public string GetClaim(string email)
        {
            var dataAccess = new DataAccess();
            string claim = dataAccess.DSGetClaim(email);
            return claim;

        }
        public string GenerateToken(string email, string password, string claim)
        {
            string hash = string.Join(":", new string[] { email, password, claim });
            string hashLeft = "";
            string hashRight = "";
            using (HMAC hmac = HMACSHA256.Create(_algorithm))
            {
                hmac.Key = Encoding.UTF8.GetBytes(password);
                hmac.ComputeHash(Encoding.UTF8.GetBytes(hash));
                hashLeft = Convert.ToBase64String(hmac.Hash);
                hashRight = string.Join(":", new string[] { email, claim });
            }
            string token = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Join(":", hashLeft, hashRight)));
            return token;
        }
    }
}