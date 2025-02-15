using Xunit;

public class AutoDataAdapterAttributeTests
{
    // [Fact(Skip = "")]
    [Fact]
    public void GivenTestDataWithInstance_WhenGetDataCalled_ThenAutoDataGenerationSkipped()
    {
        // Arrange
        IFixture fixture = new Fixture();
        AutoDataAdapterAttribute attribute = new AutoDataAdapterAttribute(fixture, SpecificTestClass.Create());
        var methodInfo = typeof(AutoDataAdapterAttributeTests).GetMethod("TestMethodWithAbstractTestClass");
        // Act
        var data = attribute.GetData(methodInfo);
        // Assert
        Assert.Equal(1, data.Count);
        Assert.Equal(methodInfo.GetParameters().Length, data[0].Count);
        Assert.False(data[0].Contains(null));
        Assert.True(data.Skip(1).All(d => d.Equals(data[0][data[0].Count - 1])));
    }
}
