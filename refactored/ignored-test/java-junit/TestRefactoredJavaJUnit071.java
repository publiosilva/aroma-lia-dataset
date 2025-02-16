import org.junit.Test;
import org.junit.Ignore;
import static org.junit.Assert.*;

public class AutoDataAdapterAttributeTests {

    // @Ignore("")
    @Test
    public void GivenTestDataWithInstance_WhenGetDataCalled_ThenAutoDataGenerationSkipped() {
        {
            // Arrange
            IFixture fixture = new Fixture();
            AutoDataAdapterAttribute attribute = new AutoDataAdapterAttribute(fixture, SpecificTestClass.Create());
            Method methodInfo = AutoDataAdapterAttributeTests.class.getDeclaredMethod("TestMethodWithAbstractTestClass");
            // Act
            List<Data> data = attribute.GetData(methodInfo);
            // Assert
            assertEquals(1, data.size());
            assertEquals(methodInfo.getParameterCount(), data.get(0).size());
            assertFalse(data.get(0).contains(null));
            assertTrue(data.stream().skip(1).allMatch(d -> d.equals(data.get(0).get(data.get(0).size() - 1))));
        }
    }
}
