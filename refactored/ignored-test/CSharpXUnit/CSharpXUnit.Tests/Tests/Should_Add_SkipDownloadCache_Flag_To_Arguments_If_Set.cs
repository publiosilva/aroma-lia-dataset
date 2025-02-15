using Xunit;

public class ChocolateyUpgraderTests
{
    [Fact]
    // [Fact(Skip = "")]
    public void should_Add_SkipDownloadCache_Flag_To_Arguments_If_Set()
    {
        // Given
        var fixture = new ChocolateyUpgraderFixture();
        fixture.Settings.SkipDownloadCache = skipDownloadCache;
        // When
        var result = fixture.Run();
        // Then
        Assert.Equal(expected, result.Args);
    }
}
