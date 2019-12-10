using System;
using System.IO;
namespace TBZ_Logging
{
    public class Logging
    {
        static String path;
        public Logging(String doc) { path = doc; }

        /// <summary>
        /// 
        /// </summary>
        // TODO: consider async Task<bool[]> Log()
        public void Log(String operation, String msg, String id) {
            DateTime timestamp = DateTime.UtcNow;
            String time = timestamp.ToString("s");
            String finallog;
            if (msg.Equals("")){
                finallog = time + " " + operation;
            }
            else
            {
                finallog = time + " " + operation + " " + msg;
            }
            File.AppendAllText(path, finallog);
        }
    }
}
