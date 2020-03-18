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
        /// <param name="endDrive"> String of target drive </param>
        public Archiver(string srcDir, int days, string endDir, string endDrive)
        {
            // check whitespace
            if (string.IsNullOrWhiteSpace(srcDir) || string.IsNullOrWhiteSpace(endDir) || days < 0 || string.IsNullOrWhiteSpace(endDrive))
            {
                throw new ArgumentException();
            }
            DirectoryInfo sDI = new DirectoryInfo(srcDir);
            DirectoryInfo eDI = new DirectoryInfo(endDir);
            DriveInfo eDrive = new DriveInfo(Path.GetPathRoot(endDir));
                //new DriveInfo(endDrive);
            // check if directories exist
            if (!sDI.Exists || !eDI.Exists || !eDrive.IsReady)
            {
                throw new ArgumentException();
            }
            else
            {
                _srcDir = sDI;
                _days = days;
                _endDir = eDI;
                _endDrive = eDrive;
            }
        }

        /// <summary>
        /// A helper method to create the destination file of the archive.
        /// </summary>
        /// <param name="dir"> String of the target directory </param>
        /// <returns> String of final destination </returns>
        protected string CreateFile(string dir)
        {
            return dir + Path.DirectorySeparatorChar + DateTime.UtcNow.ToString("_yyyyMMdd") + ".7z";
        }

        /// <summary>
        /// A method to archive to the object's destination.
        /// </summary>
        /// <returns> Bool of success or failure </returns>
        public bool Archive()
        {
            Console.WriteLine("Success 2.0.");

            if (_srcDir.Exists && _endDir.Exists && _endDrive.IsReady)
            {
                Console.WriteLine("Success 2.1.");

                // check each file in src dir
                FileInfo[] files = _srcDir.GetFiles();
                foreach (FileInfo fi in files)
                {
                    // check last access time > time
                    DateTime now = DateTime.UtcNow;
                    TimeSpan ts = now.Subtract(fi.LastWriteTimeUtc);
                    if ( fi.Extension.Equals(".csv")) //ts.Days > _days &&
                    {
                        // check size
                        if (fi.Length <= _endDrive.AvailableFreeSpace)
                        {
                            // move files
                            fi.MoveTo(_endDir.ToString() + Path.DirectorySeparatorChar + fi.Name);
                        }
                    }
                }
                Console.WriteLine("Success 2.2.");
                _endDir.MoveTo(CreateFile(_endDir.ToString()));
                // compress files
                //ZipFile.CreateFromDirectory(_endDir.ToString(), CreateFile(_endDir.ToString()));
                Console.WriteLine("Success 2.3.");

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
