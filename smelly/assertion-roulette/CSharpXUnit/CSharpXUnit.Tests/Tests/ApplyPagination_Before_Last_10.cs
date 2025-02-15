using Xunit;

namespace DefaultNamespace
{
    public class CursorPagingHandlerTests
    {
        [Fact]
        public void ApplyPagination_Before_Last_10()
        {
            {
                // arrange
                Foo[] data =
                {
                    Foo.Create(0),
                    Foo.Create(1),
                    Foo.Create(2),
                    Foo.Create(3),
                };
                // act
                var result = await Apply(data, before: ToBase64(3), last: 10);
                // assert
                Assert.Equal(0, ToFoo(result).First().Index);
                Assert.Equal(2, ToFoo(result).Last().Index);
                Assert.True(result.Info.HasNextPage);
                Assert.False(result.Info.HasPreviousPage);
                Assert.Equal(ToBase64(0), result.Info.StartCursor);
                Assert.Equal(ToBase64(2), result.Info.EndCursor);
                Assert.Equal(4, await result.GetTotalCountAsync(default));
            }
        }
    }
}
