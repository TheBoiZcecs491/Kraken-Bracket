using System;
using TBZ.KrakenBracket.Services;

namespace TBZ.KrakenBracket.Managers
{
    public class LoggingManager
    {
        readonly FlatFileLoggingService _flatFileLoggingService = new FlatFileLoggingService();
        readonly DataStoreLoggingService _dataStoreLoggingService = new DataStoreLoggingService();
        readonly string _id = "user";

        public bool Log(string operation, string message)
        {
            try
            {
                bool flatFileResult = _flatFileLoggingService.Log(operation, message, _id);
                bool dataStoreResult = _dataStoreLoggingService.Log(operation, message, _id);
                if (flatFileResult && dataStoreResult)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            } catch (Exception e)
            {
                throw e;
            }
        }

    }
}
