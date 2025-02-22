import static org.junit.jupiter.api.Assertions.assertEquals;

import org.junit.jupiter.api.Disabled;
import org.junit.jupiter.api.Test;

class ChocolateyUpgraderTests {

    @Ignore
    @Test
    public void should_Add_SkipIfNotInstalled_Flag_To_Arguments_If_Set() {
        // Given
        ChocolateyUpgraderFixture fixture = new ChocolateyUpgraderFixture();
        fixture.Settings.SkipIfNotInstalled = skipIfNotInstalled;
        // When
        var result = fixture.Run();
        // Then
        assertEquals(expected, result.Args);
    }
}
