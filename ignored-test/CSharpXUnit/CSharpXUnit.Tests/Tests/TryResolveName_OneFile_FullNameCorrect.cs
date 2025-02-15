using Xunit;

namespace DefaultNamespace
{
    public class LiveUnitTestingDirectoryResolverTests
    {
        [Fact(Skip = "")]
        public void TryResolveName_OneFile_FullNameCorrect()
        {
            {
                // arrange
                var tempDir = ArrangeLiveUnitTestDirectory("Test1.cs");
                var testName = "Test1.Foo";
                // act
                SnapshotFullName fullName = LiveUnitTestingDirectoryResolver.TryResolveName(testName);
                // assert
                fullName.Should().NotBeNull();
                fullName.FolderPath.Should().Be(Path.Combine(tempDir, "1"));
                fullName.Filename.Should().Be(testName);
            }
        }
    }
}
