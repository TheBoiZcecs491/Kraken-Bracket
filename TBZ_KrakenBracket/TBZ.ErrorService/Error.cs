using System;
using System.IO;

namespace TBZ.ErrorService
{
    public class Error
    {
    }

    public class Authn
    {
        public bool Login(bool x)
        {
            if (x == false)
            {
                throw new ArgumentException("Login failed");
            }
            return true;
        }
    }

    public class Authz
    {
        public bool Authorize(bool x)
        {
            if (x == false)
            {
                throw new ArgumentException("Authorize failed");
            }
            return true;
        }
    }

    public class Logging
    {
        public bool Log(bool x)
        {
            if (x == false)
            {
                throw new ArgumentException("Logging failed");
            }
            return true;
        }
    }

    public class Database
    {
        public bool Connection(bool x)
        {
            if (x == false)
            {
                throw new ArgumentException("Database failed");
            }
            return true;
        }

        public void Store(string path)
        {
            File.Exists(path);
            File.ReadAllLines(path);
        }
    }

}