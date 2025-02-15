using Xunit;

namespace DefaultNamespace
{
    public class Get
    {
        [Fact(Skip = "")]
        public void ItReturnsNullWhenNotFound()
        {
            {
                // Arrange
                // Act
                var result = _repo.Get(-1);
                // Assert
                Assert.Null(result);
            }
        }
    }
}
