using Xunit;

public class ExternalDataSpecificationTests
{
    [Fact]
    public void ShouldTransformUnderscoresToSpacesWhenRequiredHeadersHaveSpaces()
    {
        {
            var sut = CreateSut(new Dictionary<string, string> { { "product_name", "product" } });
            var result = sut.GetExampleRecords(new string[] { "product name" });
            Assert.NotNull(result, "Explanation message");
            Assert.Equal(3, result.GetItems().Count, "Explanation message");
            Assert.True(result.GetHeader().Contains("product name"), "Explanation message");
            Assert.Equal("Chocolate", result.GetItems()[0].GetFields()["product name"].GetValue(), "Explanation message");
        }
    }
}
