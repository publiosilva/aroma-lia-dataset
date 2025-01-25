using Xunit;

namespace nlog
{
    public class ArchiveFileNameHelper
    {
        [Fact]
        public void DeleteArchiveFilesByDate_MaxArchiveFiles_0()
        {
            {
                var tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
                var logFile = Path.Combine(tempPath, "file.txt");
                try
                {
                    string archiveFolder = Path.Combine(tempPath, "archive");
                    var fileTarget = new FileTarget
                    {
                        FileName = logFile,
                        ArchiveFileName = Path.Combine(archiveFolder, "{#}.txt"),
                        ArchiveAboveSize = 50,
                        LineEnding = LineEndingMode.LF,
                        ArchiveNumbering = ArchiveNumberingMode.Date,
                        ArchiveDateFormat = "yyyyMMddHHmmssfff", //make sure the milliseconds are set in the filename
                        Layout = "${message}",
                        MaxArchiveFiles = 0
                    };
                    SimpleConfigurator.ConfigureForTargetLogging(fileTarget, LogLevel.Debug);
                    //writing 19 times 10 bytes (9 char + linefeed) will result in 3 archive files and 1 current file
                    for (var i = 0; i < 19; ++i)
                    {
                        logger.Debug("123456789");
                        //build in a small sleep to make sure the current time is reflected in the filename
                        //do this every 5 entries
                        if (i % 5 == 0)
                        {
                            Thread.Sleep(50);
                        }
                    }
            
                    //Setting the Configuration to [null] will result in a 'Dump' of the current log entries
                    LogManager.Configuration = null; // Flush
                    var fileCount = Directory.EnumerateFiles(archiveFolder).Count();
                    Assert.Equal(3, fileCount);
                    SimpleConfigurator.ConfigureForTargetLogging(fileTarget, LogLevel.Debug);
                    //create 1 new file for archive
                    logger.Debug("1234567890");
                    LogManager.Configuration = null;
                    var fileCount2 = Directory.EnumerateFiles(archiveFolder).Count();
                    //there should be 1 more file
                    Assert.Equal(4, fileCount2);
                }
                finally
                {
                    if (File.Exists(logFile))
                    {
                        File.Delete(logFile);
                    }
            
                    if (Directory.Exists(tempPath))
                    {
                        Directory.Delete(tempPath, true);
                    }
                }
            }
        }
    }
}
