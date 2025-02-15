import org.junit.Test;
import org.mockito.Mockito;

public class Search {
    @Test
    // @Disabled
    public void itCallsSearchServiceOnce() {
        // Arrange
        // Act
        _controller.search("jos");
        // Assert
        Mockito.verify(_speakerServiceMock, Mockito.times(1)).search(Mockito.anyString());
    }
}
