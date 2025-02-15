using Xunit;

public class ChocolateyUpgraderTests {

    [Fact]
    // [Fact(Skip = "")]
    public void Should_Add_SkipHooks_Flag_To_Arguments_If_Set() {
        // Given
        var fixture = new ChocolateyUpgraderFixture();
        fixture.Settings.SkipHooks = skipHooks;
        // When
        var result = fixture.Run();
        // Then
        Assert.Equal(expected, result.Args);
    }
}
