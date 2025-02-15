using System;
using System.IO;
using Xunit;

public class ConsoleLogTest
{
    // [Fact(Skip = "Replace this with appropriate condition if necessary")]
    [Fact]
    public void TestWrite()
    {
        string message = "This is a log message";
        using (FileStream testStream = new FileStream(_testFile, FileMode.Create))
        using (StreamWriter writer = new StreamWriter(testStream))
        {
            Console.SetOut(writer);
            _log.WriteLine(message);
        }

        using (FileStream testStream = new FileStream(_testFile, FileMode.Open))
        using (StreamReader reader = new StreamReader(testStream))
        {
            string line = reader.ReadLine();
            Assert.True(line.EndsWith(message)); // consider the time stamp
        }
    }
}
