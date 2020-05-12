using System;
using System.IO;

namespace TBZ.KrakenBracket.Services
{
    public class FlatFileLoggingService
    {
        readonly DirectoryInfo _endDir;
        readonly int _tries;

        /// <summary>
        /// A default logging object that logs to the desktop
        /// in a folder called logs. Default tries is 3.
        /// </summary>
        public FlatFileLoggingService()
        {
             _endDir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + Path.DirectorySeparatorChar + "logs");
            _tries = 3;
        }

        /// <summary>
        /// A logging object requires a directory path to save logs.
        /// </summary>
        /// <param name="dir"> String of directory path </param>
        /// <param name="runs"> Integer of the number of attempts to try to log </param>
        public FlatFileLoggingService(string dir, int runs)
        {
            DirectoryInfo eDI;          // check whitespace
            if (string.IsNullOrWhiteSpace(dir))
            {
                throw new ArgumentException("Target Directory Is Null Or Blank.");
            }
            else
            {
                eDI = new DirectoryInfo(dir);
                if (!eDI.Exists)
                {
                    throw new ArgumentException("Invalid Target Directory.");
                }
            }
            if (runs < 1)
            {
                throw new ArgumentException("Invalid Amount Of Tries.");
            }
            _endDir = eDI;
            _tries = runs;
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
        /// A helper method to format the contents of a log.
        /// </summary>
        /// <param name="operation"> String of logged operation </param>
        /// <param name="msg"> String of error message (if an error occurred), "" otherwise </param>
        /// <param name="id"> String of User/System Unique Identification </param>
        /// <param name="time"> String of the current date and time from TimeStamp </param>
        /// <returns> String of the correctly formatted log contents </returns>
        protected string FormatLog(string operation, string msg, string id, string time)
        {
            string finallog;    // separated every value by comma (leave empty space for error message)
            if (msg.Equals("")) { finallog = time + ", " + operation + ", , ID_" + id + "\n"; }   // Format: Date, Time, Operation, , ID_
            else { finallog = time + ", " + operation + ", \"" + msg + "\", ID_" + id + "\n"; }  // Format: Date, Time, Operation, "Error", ID_
            return finallog;
        }

        /// <summary>
        /// A helper method to append the log to the desired file.
        /// </summary>
        /// <param name="log"> String of the correctly formatted log contents from FormatLog </param>
        /// <returns> Boolean of success or failure to log </returns>
        protected bool AppendLog(string dest, string log)
        {
            bool success = false;
            for (int i = 0; i < _tries; i++)
            {
                try
                {
                    File.AppendAllText(dest, log);      // Appends to a new file if one does not
                    success = true;                     // previously exist for this file path
                    break;
                }
                catch
                {
                    Console.Write("Path Failed " + (i + 1) + " Time(s).\n");
                    if (i == 2)
                    {
                        throw new FileNotFoundException("Unable to write to file.");
                    }
                }
            }
            return success;
        }

        protected string CreateDestination(string dir)
        {
            return dir + Path.DirectorySeparatorChar + DateTime.UtcNow.ToString("_yyyyMMdd") + ".csv";
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
            bool success;
            //test operation and id for validity
            if (string.IsNullOrWhiteSpace(operation) || string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException();
            }
            string time = TimeStamp();
            string log = FormatLog(operation, msg, id, time);
            try
            {
                string dest = CreateDestination(_endDir.ToString());
                success = AppendLog(dest, log);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                success = false;
            }
            return success;
        }
    }
}