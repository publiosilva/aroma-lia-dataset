import org.junit.Test;
import static org.junit.Assert.assertEquals;

public class ChocolateyUpgraderTests {
    
    @Test
    @Disabled
    public void should_Add_SkipDownloadCache_Flag_To_Arguments_If_Set() {
        {
            // Given
            ChocolateyUpgraderFixture fixture = new ChocolateyUpgraderFixture();
            fixture.getSettings().setSkipDownloadCache(skipDownloadCache);
            // When
            var result = fixture.run();
            // Then
            assertEquals(expected, result.getArgs());
        }
    }
}
