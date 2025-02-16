import org.junit.Test;
import org.junit.Ignore;
import static org.junit.Assert.assertEquals;

public class ChocolateyUpgraderTests {

    @Test
    @Ignore("") 
    public void shouldAddSkipCompatibilityFlagToArgumentsIfSet() {
        // Given
        ChocolateyUpgraderFixture fixture = new ChocolateyUpgraderFixture();
        fixture.Settings.SkipCompatibilityChecks = skipCompatibiity;
        // When
        var result = fixture.Run();
        // Then
        assertEquals(expected, result.Args);
    }
}
