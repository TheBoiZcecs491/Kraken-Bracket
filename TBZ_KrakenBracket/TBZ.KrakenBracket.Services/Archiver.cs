using System;
using System.IO;
using System.IO.Compression;

namespace TBZ.ArchiverService
{
    public class Archiver
    {
        //read-only field
        readonly DirectoryInfo _srcDir;
        readonly int _days;
        readonly DirectoryInfo _endDir;
        readonly DriveInfo _endDrive;

        /// <summary>
        /// A archiving object requires a directory to save logs from, the amount of time
        /// (in days) that you want to archive from, the target directory, and the target drive.
        /// </summary>
        /// <param name="srcDir"> String of the directory of logs </param>
        /// <param name="days"> Int of days old to save </param>
        /// <param name="endDir"> String of target directory </param>
        public Archiver(string srcDir, int days, string endDir)
        {
            DirectoryInfo sDI;
            DirectoryInfo eDI;
            // check whitespace
            if (string.IsNullOrWhiteSpace(srcDir))
            {
                Console.WriteLine("Source Directory Is Null or Blank.");
                throw new ArgumentException();
            }
            else
            {
                sDI = new DirectoryInfo(srcDir);
                if (!sDI.Exists)
                {
                    Console.WriteLine("Invalid Source Directory.");
                    throw new ArgumentException();
                }
            }
            if (string.IsNullOrWhiteSpace(endDir))
            {
                Console.WriteLine("Target Directory Is Null Or Blank.");
                throw new ArgumentException();
            }
            else
            {
                eDI = new DirectoryInfo(endDir);
                if (!eDI.Exists)
                {
                    Console.WriteLine("Invalid Target Directory.");
                    throw new ArgumentException();
                }
            }
            if (days < -1)
            {
                Console.WriteLine("Invalid Number Of Days.");
                throw new ArgumentException();
            }

            DriveInfo endDrive = new DriveInfo(Path.GetPathRoot(endDir));
            _srcDir = sDI;
            _days = days;
            _endDir = eDI;
            _endDrive = endDrive;
        }

        /// <summary>
        /// A helper method to create the destination file of the archive.
        /// </summary>
        /// <param name="dir"> String of the target directory </param>
        /// <returns> String of final destination </returns>
        protected string CreateFolder(string dir)
        {
            return dir + Path.DirectorySeparatorChar + DateTime.UtcNow.ToString("_yyyyMMdd_HHmmssff");
        }

        /// <summary>
        /// A helper method to create the extension for the archive files.
        /// </summary>
        /// <returns></returns>
        protected string CreateExtension()
        {
            return ".7z";
        }

        /// <summary>
        /// A method to archive to the object's destination.
        /// </summary>
        /// <returns> Bool of success or failure </returns>
        public bool Archive()
        {
            string folder = CreateFolder(_endDir.ToString());
            Directory.CreateDirectory(folder);
            if (_srcDir.Exists && _endDir.Exists && _endDrive.IsReady)
            {
                // check each file in src dir
                FileInfo[] files = _srcDir.GetFiles();
                foreach (FileInfo fi in files)
                {
                    string old = fi.FullName;
                    // check last access time > time
                    DateTime now = DateTime.UtcNow;
                    TimeSpan ts = now.Subtract(fi.LastWriteTimeUtc);
                    if (ts.Days > _days && fi.Extension.Equals(".csv"))
                    {
                        // check size
                        if (fi.Length <= _endDrive.AvailableFreeSpace)
                        {
                            // move files
                            fi.MoveTo(folder + Path.DirectorySeparatorChar + fi.Name);
                            File.Delete(old);
                        }
                    }
                }
                // compress files
                ZipFile.CreateFromDirectory(folder, folder + CreateExtension());
                Directory.Delete(folder, true);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}