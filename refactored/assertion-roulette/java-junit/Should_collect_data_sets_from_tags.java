import org.junit.Test;
import static org.junit.Assert.*;

public class SpecificationProviderTests {

    @Test
    public void shouldCollectDataSetsFromTags() {
        {
            var sut = createSut();
            var result = sut.getSpecification(new Tag[] { new Tag(null, "@DataSource:path\\to\\file.csv"), new Tag(null, "@DataSet:data-set-name") }, SOURCE_FILE_PATH);
            assertNotNull("Explanation message", result);
            assertNotNull("Explanation message", result.getDataSet());
            assertEquals("Explanation message", "data-set-name", result.getDataSet());
        }
    }
}
