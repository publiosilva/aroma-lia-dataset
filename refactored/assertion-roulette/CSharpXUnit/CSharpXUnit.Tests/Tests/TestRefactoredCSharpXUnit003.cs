using Xunit;

public class CursorPagingHandlerTests {

    [Fact]
    public void ApplyPagination_Before_Last_10() {
        // arrange
        Foo[] data = {
            Foo.Create(0),
            Foo.Create(1),
            Foo.Create(2),
            Foo.Create(3),
        };
        // act
        var result = Apply(data, ToBase64(3), 10);
        // assert
        Assert.Equal(0, ToFoo(result)[0].Index, "Explanation message");
        Assert.Equal(2, ToFoo(result)[ToFoo(result).Count - 1].Index, "Explanation message");
        Assert.True(result.Info.HasNextPage(), "Explanation message");
        Assert.False(result.Info.HasPreviousPage(), "Explanation message");
        Assert.Equal(ToBase64(0), result.Info.StartCursor, "Explanation message");
        Assert.Equal(ToBase64(2), result.Info.EndCursor, "Explanation message");
        Assert.Equal(4, result.TotalCountAsync(), "Explanation message");
    }
}
