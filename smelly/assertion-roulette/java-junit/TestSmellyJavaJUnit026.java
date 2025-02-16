import org.junit.Test;
import static org.junit.Assert.*;

public class SyntaxTypeReferenceTests {

    @Test
    public void typeReference_Equals_Scope_Different() {
        {
            // arrange
            TypeReference x = TypeReference.create("Foo", TypeContext.NONE, "a");
            TypeReference y = TypeReference.create("Foo", TypeContext.OUTPUT, "a");
            TypeReference z = TypeReference.create("Foo", TypeContext.INPUT);
            // act
            boolean xy = x.equals((TypeReference) y);
            boolean xz = x.equals((TypeReference) z);
            boolean yz = y.equals((TypeReference) z);
            // assert
            assertTrue(xy);
            assertFalse(xz);
            assertFalse(yz);
        }
    }
}
