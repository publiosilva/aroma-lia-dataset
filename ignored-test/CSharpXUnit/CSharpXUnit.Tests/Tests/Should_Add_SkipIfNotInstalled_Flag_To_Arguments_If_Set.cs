using Xunit;

namespace DefaultNamespace
{
    public class ChocolateyUpgraderTests
    {
        [Fact(Skip = "")]
        public void Should_Add_SkipIfNotInstalled_Flag_To_Arguments_If_Set()
        {
            {
                // Given
                var fixture = new ChocolateyUpgraderFixture();
                fixture.Settings.SkipIfNotInstalled = skipIfNotInstalled;
                // When
                var result = fixture.Run();
                // Then
                Assert.Equal(expected, result.Args);
            }
        }
    }
}
