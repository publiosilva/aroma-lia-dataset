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
        var result = await apply(data, before: toBase64(3), last: 10);
        // assert
        assertEquals(0, toFoo(result).get(0).getIndex());
        assertEquals(2, toFoo(result).get(toFoo(result).size() - 1).getIndex());
        assertTrue(result.getInfo().hasNextPage());
        assertFalse(result.getInfo().hasPreviousPage());
        assertEquals(toBase64(0), result.getInfo().getStartCursor());
        assertEquals(toBase64(2), result.getInfo().getEndCursor());
        assertEquals(4, await result.getTotalCountAsync(default));
    }
}
