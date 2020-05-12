using System;
using System.IO;
using TBZ.KrakenBracket.DatabaseAccess;

namespace TBZ.KrakenBracket.Services
{
    public class DataStoreLoggingService
    {
        readonly int _tries;
        readonly DataStore _dataStore;

        /// <summary>
        /// A default logging object that logs to the data store.
        /// Default tries is 3.
        /// </summary>
        public DataStoreLoggingService()
        {
            _dataStore = new DataStore();
            _tries = 3;
        }

        /// <summary>
        /// A helper method to date and time stamp the current log using DateTime struct.
        /// </summary>
        /// <returns> String of the current date and time </returns>
        protected string TimeStamp()
        {
            DateTime timestamp = DateTime.UtcNow;                           // Current Date & Time (UTC)
            string time = timestamp.ToString("yyyy-MM-dd, HH:mm:ss:ff");    // Format: Year-Month-Day, Hour:Minute:Second:Milisecond
            return time;
        }

        /// <summary>
        /// A logging object can "log" with a specific operation,
        /// error message (if applicable), and a user id.
        /// </summary>
        /// <param name="operation"> String of logged operation </param>
        /// <param name="msg"> String of error message (if an error occurred), "" otherwise </param>
        /// <param name="id"> String of User/System Unique Identification </param>
        /// <returns> Boolean of success or failure to log </returns>
        public bool Log(string operation, string msg, string id)
        {
            //test operation and id for validity
            if (string.IsNullOrWhiteSpace(operation) || string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException();
            }
            string time = TimeStamp();
            try
            {
                bool result = _dataStore.LogDataStore(time, operation, msg, id);
                int tries = 1;
                while (!result && tries < _tries)
                {
                    result = _dataStore.LogDataStore(time, operation, msg, id);
                    tries++;
                }
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}