using System;
using TBZ.KrakenBracket.Services;
namespace ClientApp
{
    public class ArchivingScript
    {
        public static void Main(string[] args)
        {
            // Make sure only days, source Directory, and targetDirectory are entered into program
            if (args.Length != 4)
            {
                return;
            }
            try
            {
                int days = Convert.ToInt32(args[1]);
                string sourceDirectory = args[2];
                string targetDirectory = args[3];
                ArchiveService _archiveService = new ArchiveService(sourceDirectory, days, targetDirectory);
                _archiveService.Archive();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
