using Xunit;
using static Xunit.Assert;

public class LiveUnitTestingDirectoryResolverTests
{
    [Fact]
    // [Fact(Skip = "")]
    public void tryResolveName_OneFile_FullNameCorrect()
    {
        // arrange
        var tempDir = ArrangeLiveUnitTestDirectory("Test1.cs");
        var testName = "Test1.Foo";
        // act
        SnapshotFullName fullName = LiveUnitTestingDirectoryResolver.TryResolveName(testName);
        // assert
        NotNull(fullName);
        Equals(Path.Combine(tempDir, "1"), fullName.GetFolderPath());
        Equals(testName, fullName.GetFilename());
    }
}
