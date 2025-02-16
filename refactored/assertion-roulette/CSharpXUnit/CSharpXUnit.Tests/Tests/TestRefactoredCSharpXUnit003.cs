using Xunit;

namespace DefaultNamespace
{
    public class CursorPagingHandlerTests
    {
        [Fact]
        public void ApplyPagination_Before_Last_101()
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
            }
        }

        [Fact]
        public void ApplyPagination_Before_Last_102()
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
                Assert.Equal(2, ToFoo(result).Last().Index);
            }
        }

        [Fact]
        public void ApplyPagination_Before_Last_103()
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
                Assert.True(result.Info.HasNextPage);
            }
        }

        [Fact]
        public void ApplyPagination_Before_Last_104()
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
                Assert.False(result.Info.HasPreviousPage);
            }
        }

        [Fact]
        public void ApplyPagination_Before_Last_105()
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
                Assert.Equal(ToBase64(0), result.Info.StartCursor);
            }
        }

        [Fact]
        public void ApplyPagination_Before_Last_106()
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
                Assert.Equal(ToBase64(2), result.Info.EndCursor);
            }
        }

        [Fact]
        public void ApplyPagination_Before_Last_107()
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
                Assert.Equal(4, await result.GetTotalCountAsync(default));
            }
        }
    }
}
