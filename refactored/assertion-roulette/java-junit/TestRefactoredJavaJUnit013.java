import org.junit.Test;
import static org.junit.Assert.*;

public class SyntaxTypeReferenceTests {
    
    @Test
    public void Object_Equals_Scope_Different() {
        {
            // arrange
            var x = TypeReference.create("Foo", TypeContext.None, "a");
            var y = TypeReference.create("Foo", TypeContext.Output, "a");
            var z = TypeReference.create("Foo", TypeContext.Input);
            // act
            var xy = x.equals(y);
            var xz = x.equals(z);
            var yz = y.equals(z);
            // assert
            assertTrue("Explanation message", xy);
            assertFalse("Explanation message", xz);
            assertFalse("Explanation message", yz);
        }
    }
}
