using Xunit;

namespace DefaultNamespace
{
    public class ConsoleLogTest
    {
        [Fact(Skip = "")]
        public void TestWrite()
        {
            {
                var message = "This is a log message";
                using (var testStream = new FileStream(_testFile, FileMode.OpenOrCreate, FileAccess.Write))
                using (var writer = new StreamWriter(testStream))
                {
                    Console.SetOut(writer);
                    // simply test that we do write in the file. We need to close the stream to be able to read it
                    _log.WriteLine(message);
                }
            
                using (var testStream = new FileStream(_testFile, FileMode.OpenOrCreate, FileAccess.Read))
                using (var reader = new StreamReader(testStream))
                {
                    var line = reader.ReadLine();
                    Assert.EndsWith(message, line); // consider the time stamp
                }
            }
        }
    }
}
