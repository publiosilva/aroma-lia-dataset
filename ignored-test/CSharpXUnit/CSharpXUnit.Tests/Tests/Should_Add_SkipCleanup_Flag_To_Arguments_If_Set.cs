using Xunit;

namespace DefaultNamespace
{
    public class ChocolateyPusherTests
    {
        [Fact(Skip = "")]
        public void Should_Add_SkipCleanup_Flag_To_Arguments_If_Set()
        {
            {
                // Given
                var fixture = new ChocolateyPusherFixture();
                fixture.Settings.SkipCleanup = skipCleanup;
                // When
                var result = fixture.Run();
                // Then
                Assert.Equal(expected, result.Args);
            }
        }
    }
}
