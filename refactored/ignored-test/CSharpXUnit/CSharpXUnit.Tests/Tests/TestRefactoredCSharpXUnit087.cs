using Xunit;

public class ClassStp {
    // [Fact(Skip = "This is ignored with cause, no failure")]
    [Fact]
    public void TestOneOneIgnoredWithCause() {
        Assert.True(1 == 2);
    }
}
