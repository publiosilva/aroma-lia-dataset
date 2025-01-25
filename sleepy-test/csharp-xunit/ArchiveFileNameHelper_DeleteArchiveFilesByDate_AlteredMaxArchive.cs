using Xunit;

namespace nlog
{
    public class ArchiveFileNameHelper
    {
        [Fact]
        public void DeleteArchiveFilesByDate_AlteredMaxArchive()
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
                        MaxArchiveFiles = 5
                    };
                    SimpleConfigurator.ConfigureForTargetLogging(fileTarget, LogLevel.Debug);
                    //writing 29 times 10 bytes (9 char + linefeed) will result in 3 archive files and 1 current file
                    for (var i = 0; i < 29; ++i)
                    {
                        logger.Debug("123456789");
                        //build in a small sleep to make sure the current time is reflected in the filename
                        //do this every 5 entries
                        if (i % 5 == 0)
                            Thread.Sleep(50);
                    }
            
                    //Setting the Configuration to [null] will result in a 'Dump' of the current log entries
                    LogManager.Configuration = null;
                    var files = Directory.GetFiles(archiveFolder).OrderBy(s => s);
                    //the amount of archived files may not exceed the set 'MaxArchiveFiles'
                    Assert.Equal(fileTarget.MaxArchiveFiles, files.Count());
                    //alter the MaxArchivedFiles
                    fileTarget.MaxArchiveFiles = 2;
                    SimpleConfigurator.ConfigureForTargetLogging(fileTarget, LogLevel.Debug);
                    //writing just one line of 11 bytes will trigger the cleanup of old archived files
                    //as stated by the MaxArchiveFiles property, but will only delete the oldest files
                    logger.Debug("1234567890");
                    LogManager.Configuration = null; // Flush
                    var files2 = Directory.GetFiles(archiveFolder).OrderBy(s => s);
                    Assert.Equal(fileTarget.MaxArchiveFiles, files2.Count());
                    //the oldest files should be deleted
                    Assert.DoesNotContain(files.ElementAt(0), files2);
                    Assert.DoesNotContain(files.ElementAt(1), files2);
                    Assert.DoesNotContain(files.ElementAt(2), files2);
                    Assert.DoesNotContain(files.ElementAt(3), files2);
                    //one files should still be there
                    Assert.Equal(files.ElementAt(4), files2.ElementAt(0));
                    //one new archive file should be created
                    Assert.DoesNotContain(files2.ElementAt(1), files);
                }
                finally
                {
                    if (File.Exists(logFile))
                        File.Delete(logFile);
                    if (Directory.Exists(tempPath))
                        Directory.Delete(tempPath, true);
                }
            }
        }
    }
}
