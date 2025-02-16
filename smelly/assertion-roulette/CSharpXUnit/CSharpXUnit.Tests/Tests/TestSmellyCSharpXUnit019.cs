using Xunit;

namespace DefaultNamespace
{
    public class ExternalDataSpecificationTests
    {
        [Fact]
        public void Should_transform_underscores_to_spaces_when_required_headers_have_spaces()
        {
            {
                var sut = CreateSut(new Dictionary<string, string> { { "product_name", "product" }, });
                var result = sut.GetExampleRecords(new[] { "product name" });
                Assert.NotNull(result);
                Assert.Equal(3, result.Items.Count);
                Assert.Contains("product name", result.Header);
                Assert.Equal("Chocolate", result.Items[0].Fields["product name"].Value);
            }
        }
    }
}
