using System;
using TBZ.DatabaseAccess;
using TBZ.HashingService;
using TBZ.KrakenBracket.DataHelpers;

namespace TBZ.Manager.Hashing
{
    public class HashingManager
    {
        public class hashUser : User
        {
            public string salt { get; set; }
        }

       // new MessageSalt


    }
}