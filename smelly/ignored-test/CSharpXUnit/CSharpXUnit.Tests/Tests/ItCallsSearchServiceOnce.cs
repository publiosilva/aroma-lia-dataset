using Xunit;

namespace DefaultNamespace
{
    public class Search
    {
        [Fact(Skip = "")]
        public void ItCallsSearchServiceOnce()
        {
            {
                // Arrange
                // Act
                _controller.Search("jos");
                // Assert
                _speakerServiceMock.Verify(mock => mock.Search(It.IsAny<string>()), Times.Once());
            }
        }
    }
}
