import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.Disabled;
import static org.junit.jupiter.api.Assertions.assertEquals;

public class ChocolateyApiKeySetterTests {

    @Test
    @Disabled("")
    public void shouldAddSkipCompatibilityFlagToArgumentsIfSet() {
        // Given
        ChocolateyApiKeySetterFixture fixture = new ChocolateyApiKeySetterFixture();
        fixture.getSettings().setSkipCompatibilityChecks(skipCompatibility);

        // When
        Result result = fixture.run();

        // Then
        assertEquals(expected, result.getArgs());
    }
}
