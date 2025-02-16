using Xunit;

public class ChocolateyUpgraderTests {

    [Fact]
    // [Fact(Skip = "")]
    public void shouldAddSkipCompatibilityFlagToArgumentsIfSet() {
        // Given
        var fixture = new ChocolateyUpgraderFixture();
        fixture.Settings.SkipCompatibilityChecks = skipCompatibiity;
        // When
        var result = fixture.Run();
        // Then
        Assert.Equal(expected, result.Args);
    }
}
