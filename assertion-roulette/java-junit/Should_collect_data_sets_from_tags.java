import org.junit.Test;
import static org.junit.Assert.*;

public class SpecificationProviderTests {

    @Test
    public void shouldCollectDataSetsFromTags() {
        {
            var sut = createSut();
            var result = sut.getSpecification(new Tag[] { new Tag(null, "@DataSource:path\\to\\file.csv"), new Tag(null, "@DataSet:data-set-name") }, SOURCE_FILE_PATH);
            assertNotNull(result);
            assertNotNull(result.getDataSet());
            assertEquals("data-set-name", result.getDataSet());
        }
    }
}
