using System;
using System.IO;

namespace TBZ.ArchivingService
{
    public class Archiving
    {
        static string src;
        static int days;

        /// <summary>
        /// A archiving object requires a path to save logs from and the amount of time
        /// (in days) that you want to archive from.
        /// </summary>
        /// <param name="path"> String of log .csv file </param>
        /// <param name="time"> Int of days to save from </param>
        public Archiving(string path, int time)
        {
            if (string.IsNullOrWhiteSpace(path) || !path.Contains(".csv") || time < 1)
            {
                throw new ArgumentException();
            }
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }
            src = path;
            days = time;
        }

        /// <summary>
        /// A helper method to create the destination file of the archive.
        /// </summary>
        /// <param name="dest"> String of the path to desired destination </param>
        /// <returns> String of final destination </returns>
        protected string CreateFile(string dest)
        {
            string file = DateTime.UtcNow.ToString("_yyyyMMdd");
            file += ".csv";
            dest += file;
            return dest;
        }

        /// <summary>
        /// A helper method to grab the required logs to archive using
        /// the amount of days to grab from.
        /// </summary>
        /// <param name="sr"> StreamReader of the file to read from </param>
        /// <returns> String of logs to archive </returns>
        protected string GetContents(StreamReader sr)
        {
            string content = "";
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] dates = line.Substring(0, line.IndexOf(',')).Split('-');
                DateTime date = new DateTime(Int32.Parse(dates[0]), Int32.Parse(dates[1]), Int32.Parse(dates[2]));
                DateTime now = DateTime.UtcNow;
                TimeSpan ts = now.Subtract(date);
                if (ts.Days > days - 1)
                {
                    content = content + line + "\n";
                }
                else
                {
                    break;
                }

            }
            return content;
        }

        /// <summary>
        /// A helper method to write the log to the archive file.
        /// </summary>
        /// <param name="file"> The path to the target destination </param>
        /// <param name="contents"> The logs to archive to file </param>
        /// <returns> Bool of success or failure </returns>
        protected bool WriteArchive(string file, string contents)
        {
            //kronjob
            try
            {
                File.AppendAllText(file, contents);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// An archiving object can "archive" when given a destination to archive.
        /// </summary>
        /// <param name="dest"> The path to a specific folder or destination </param>
        /// <returns> Bool of success or failure </returns>
        public bool Archive(string dest)
        {
            if (string.IsNullOrWhiteSpace(dest))
            {
                throw new ArgumentException();
            }
            string contents = "";
            using (var fs = new FileStream(src, FileMode.Open, FileAccess.Read))
            using (var sr = new StreamReader(fs))
            {
                contents = GetContents(sr);
            }
            string file = CreateFile(dest);
            bool success = WriteArchive(file, contents);
            return success;
        }
    }
}
