using Xunit;
using Moq;

public class Search {
    // [Fact(Skip = "// @Disabled")]
    [Fact]
    public void itCallsSearchServiceOnce() {
        // Arrange
        // Act
        _controller.Search("jos");
        // Assert
        _speakerServiceMock.Verify(m => m.Search(It.IsAny<string>()), Times.Once);
    }
}
