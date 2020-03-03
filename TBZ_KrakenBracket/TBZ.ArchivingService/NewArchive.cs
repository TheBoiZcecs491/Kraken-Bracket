using System;
using System.IO;
using System.IO.Compression;
namespace TBZ.ArchivingService
{
    public class NewArchive
    {
        static DirectoryInfo _srcDir;
        static int _time;
        static DirectoryInfo _endDir;
        static DriveInfo _endDrive;

        public NewArchive(string srcDir, int time, string endDir, string endDrive)
        {
            // check whitespace
            DirectoryInfo sDI = new DirectoryInfo(srcDir);
            DirectoryInfo eDI = new DirectoryInfo(endDir);
            DriveInfo eDrive = new DriveInfo(endDrive);
            if (!sDI.Exists || !eDI.Exists)
            {
                //error
            }
            else
            {
                _srcDir = sDI;
                _time = time;
                _endDir = eDI;
                _endDrive = eDrive;
            }
        }

        public bool doArchive()
        {
            string extension = "/date";
            // check each file in src dir
            FileInfo[] files = _srcDir.GetFiles();
            foreach (FileInfo fi in files)
            {
                // check last access time > time
                DateTime now = DateTime.UtcNow;
                TimeSpan ts = now.Subtract(fi.LastWriteTimeUtc);
                if (ts.Days > _time)
                {
                    // move file to specific directory to be compressed
                    fi.MoveTo(_srcDir.ToString() + extension);
                }
            }
            // compress files
            ZipFile.CreateFromDirectory(_srcDir.ToString() + extension, _srcDir.ToString() + extension + ".zip");
            // check size
            files = _srcDir.GetFiles();
            foreach(FileInfo fi in files)
            {
                if (fi.FullName.EndsWith(".zip"))
                {
                    long srcSize = fi.Length;
                    // check end drive size
                    long endSize = _endDrive.AvailableFreeSpace;
                    // compare
                    if (endSize > srcSize)
                    {
                        // move to end directory
                        fi.MoveTo(_endDir.ToString());
                    }
                }
            }
            //Directory.Exists();
            return true;
        }
    }
}
