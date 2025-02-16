import org.junit.Test;
import static org.junit.Assert.assertEquals;

public class ChocolateyUpgraderTests {

    @Test
    // @Ignore
    public void Should_Add_SkipHooks_Flag_To_Arguments_If_Set() {
        // Given
        ChocolateyUpgraderFixture fixture = new ChocolateyUpgraderFixture();
        fixture.Settings.SkipHooks = skipHooks;
        // When
        var result = fixture.Run();
        // Then
        assertEquals(expected, result.Args);
    }
}
