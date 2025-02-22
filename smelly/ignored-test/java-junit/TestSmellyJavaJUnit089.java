import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

@Ignore
class LiveUnitTestingDirectoryResolverTests {

    @Test
    public void tryResolveName_OneFile_FullNameCorrect() {
        // arrange
        var tempDir = ArrangeLiveUnitTestDirectory("Test1.cs");
        var testName = "Test1.Foo";
        // act
        SnapshotFullName fullName = LiveUnitTestingDirectoryResolver.tryResolveName(testName);
        // assert
        assertNotNull(fullName);
        assertEquals(Path.Combine(tempDir, "1"), fullName.getFolderPath());
        assertEquals(testName, fullName.getFilename());
    }
}
