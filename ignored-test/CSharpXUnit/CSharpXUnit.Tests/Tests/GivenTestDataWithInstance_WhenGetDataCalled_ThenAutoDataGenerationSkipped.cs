using Xunit;

namespace DefaultNamespace
{
    public class AutoDataAdapterAttributeTests
    {
        [Fact(Skip = "")]
        public void GivenTestDataWithInstance_WhenGetDataCalled_ThenAutoDataGenerationSkipped()
        {
            {
                // Arrange
                IFixture fixture = new Fixture();
                var attribute = new AutoDataAdapterAttribute(fixture, SpecificTestClass.Create());
                var methodInfo = typeof(AutoDataAdapterAttributeTests).GetMethod(nameof(this.TestMethodWithAbstractTestClass), BindingFlags.Instance | BindingFlags.NonPublic);
                // Act
                var data = attribute.GetData(methodInfo).ToArray();
                // Assert
                data.Should().HaveCount(1).And.Subject.First().Should().HaveCount(methodInfo.GetParameters().Length).And.NotContainNulls().And.Subject.Skip(1).Should().AllBeEquivalentTo(data.First().Last());
            }
        }
    }
}
