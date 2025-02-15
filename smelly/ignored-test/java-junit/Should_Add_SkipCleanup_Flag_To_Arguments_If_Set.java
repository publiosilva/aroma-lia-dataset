import org.junit.Test;
import static org.junit.Assert.assertEquals;
import org.junit.Ignore;

public class ChocolateyPusherTests {

    @Ignore(" ")
    @Test
    public void shouldAddSkipCleanupFlagToArgumentsIfSet() {
        {
            // Given
            ChocolateyPusherFixture fixture = new ChocolateyPusherFixture();
            fixture.getSettings().setSkipCleanup(skipCleanup);
            // When
            var result = fixture.run();
            // Then
            assertEquals(expected, result.getArgs());
        }
    }
}
