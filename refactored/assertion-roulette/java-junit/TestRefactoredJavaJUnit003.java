import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

class CursorPagingHandlerTests {

    @Test
    void applyPagination_Before_Last_10() {
        // arrange
        Foo[] data = {
            Foo.create(0),
            Foo.create(1),
            Foo.create(2),
            Foo.create(3),
        };
        // act
        var result = apply(data, toBase64(3), 10);
        // assert
        assertEquals("Explanation message", 0, toFoo(result).get(0).getIndex());
        assertEquals("Explanation message", 2, toFoo(result).get(toFoo(result).size() - 1).getIndex());
        assertTrue("Explanation message", result.getInfo().hasNextPage());
        assertFalse("Explanation message", result.getInfo().hasPreviousPage());
        assertEquals("Explanation message", toBase64(0), result.getInfo().getStartCursor());
        assertEquals("Explanation message", toBase64(2), result.getInfo().getEndCursor());
        assertEquals("Explanation message", 4, result.getTotalCountAsync());
    }
}
