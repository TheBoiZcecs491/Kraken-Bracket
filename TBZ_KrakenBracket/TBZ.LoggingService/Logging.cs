using System;
using System.IO;

namespace TBZ.LoggingService
{
    public class Logging
    {
        static string path;
        static int tries;

        /// <summary>
        /// A logging object requires a document path to save logs.
        /// </summary>
        /// <param name="doc"> String of document path </param>
        /// <param name="runs"> Integer of the number of attempts to try to log </param>
        public Logging(string doc, int runs)
        {
            if (string.IsNullOrWhiteSpace(doc) || !doc.Contains(".csv"))
            {
                throw new ArgumentException("Invalid document type.");
            }
            if (runs < 1)
            {
                throw new ArgumentException("Invalid amount of tries.");
            }
            path = doc;
            tries = runs;
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
            if (msg.Equals("")) { finallog = time + ", " + operation + ", , ID_" + id + ",\n"; }   // Format: Date, Time, Operation, , ID_
            else { finallog = time + ", " + operation + ", \"" + msg + "\", ID_" + id + ",\n"; }  // Format: Date, Time, Operation, "Error", ID_
            return finallog;
        }

        /// <summary>
        /// A helper method to append the log to the desired file.
        /// </summary>
        /// <param name="log"> String of the correctly formatted log contents from FormatLog </param>
        /// <returns> Boolean of success or failure to log </returns>
        protected bool AppendLog(string log)
        {
            bool success = false;
            for (int i = 0; i < tries; i++)
            {
                try
                {
                    File.AppendAllText(path, log);      // Appends to a new file if one does not
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
                success = AppendLog(log);
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
