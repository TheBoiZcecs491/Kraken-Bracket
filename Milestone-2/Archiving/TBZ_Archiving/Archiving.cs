using System;
using System.IO;

namespace TBZ_Archiving
{
    public class Archiving
    {
        public Archiving()
        {

        }

        public void Archive()
        {
            String path = "";       // src from logging
            String path2 = "";      // dest according to YYYYMMDD
            File.Copy(path, path2); // copy from src to dest
        }
    }
}
