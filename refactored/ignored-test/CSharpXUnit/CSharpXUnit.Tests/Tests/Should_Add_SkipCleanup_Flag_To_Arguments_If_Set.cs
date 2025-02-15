using Xunit;

public class ChocolateyPusherTests {

    // [Fact(Skip = " ")]
    [Fact]
    public void shouldAddSkipCleanupFlagToArgumentsIfSet() {
        {
            // Given
            var fixture = new ChocolateyPusherFixture();
            fixture.GetSettings().SetSkipCleanup(skipCleanup);
            // When
            var result = fixture.Run();
            // Then
            Assert.Equal(expected, result.GetArgs());
        }
    }
}
