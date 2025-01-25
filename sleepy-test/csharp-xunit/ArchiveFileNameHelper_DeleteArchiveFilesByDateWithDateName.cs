using Xunit;

namespace nlog
{
    public class ArchiveFileNameHelper
    {
        [Fact]
        public void DeleteArchiveFilesByDateWithDateName()
        {
            {
                const int maxArchiveFiles = 3;
                LogManager.ThrowExceptions = true;
                var tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
                try
                {
                    var logFile = Path.Combine(tempPath, "${date:format=yyyyMMddHHmmssfff}.txt");
                    var fileTarget = new FileTarget
                    {
                        FileName = logFile,
                        ArchiveFileName = Path.Combine(tempPath, "{#}.txt"),
                        ArchiveEvery = FileArchivePeriod.Year,
                        LineEnding = LineEndingMode.LF,
                        ArchiveNumbering = ArchiveNumberingMode.Date,
                        ArchiveDateFormat = "yyyyMMddHHmmssfff", //make sure the milliseconds are set in the filename
                        Layout = "${message}",
                        MaxArchiveFiles = maxArchiveFiles
                    };
                    SimpleConfigurator.ConfigureForTargetLogging(fileTarget, LogLevel.Debug);
                    for (var i = 0; i < 4; ++i)
                    {
                        logger.Debug("123456789");
                        //build in a  sleep to make sure the current time is reflected in the filename
                        Thread.Sleep(50);
                    }
            
                    //Setting the Configuration to [null] will result in a 'Dump' of the current log entries
                    LogManager.Configuration = null; // Flush
                    var files = Directory.GetFiles(tempPath).OrderBy(s => s);
                    //we expect 3 archive files, plus one current file
                    Assert.Equal(maxArchiveFiles + 1, files.Count());
                    SimpleConfigurator.ConfigureForTargetLogging(fileTarget, LogLevel.Debug);
                    //writing 50ms later will trigger the cleanup of old archived files
                    //as stated by the MaxArchiveFiles property, but will only delete the oldest file
                    Thread.Sleep(50);
                    logger.Debug("123456789");
                    LogManager.Configuration = null; // Flush
                    var files2 = Directory.GetFiles(tempPath).OrderBy(s => s);
                    Assert.Equal(maxArchiveFiles + 1, files2.Count());
                    //the oldest file should be deleted
                    Assert.DoesNotContain(files.ElementAt(0), files2);
                    //two files should still be there
                    Assert.Equal(files.ElementAt(1), files2.ElementAt(0));
                    Assert.Equal(files.ElementAt(2), files2.ElementAt(1));
                    Assert.Equal(files.ElementAt(3), files2.ElementAt(2));
                    //one new file should be created
                    Assert.DoesNotContain(files2.ElementAt(3), files);
                }
                finally
                {
                    if (Directory.Exists(tempPath))
                        Directory.Delete(tempPath, true);
                }
            }
        }
    }
}
