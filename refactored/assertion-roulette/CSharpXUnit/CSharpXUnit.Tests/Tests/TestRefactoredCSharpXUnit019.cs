using Xunit;

namespace DefaultNamespace
{
    public class ExternalDataSpecificationTests
    {
        [Fact]
        public void Should_transform_underscores_to_spaces_when_required_headers_have_spaces1()
        {
            {
                var sut = CreateSut(new Dictionary<string, string> { { "product_name", "product" }, });
                var result = sut.GetExampleRecords(new[] { "product name" });
                Assert.NotNull(result);
            }
        }

        [Fact]
        public void Should_transform_underscores_to_spaces_when_required_headers_have_spaces2()
        {
            {
                var sut = CreateSut(new Dictionary<string, string> { { "product_name", "product" }, });
                var result = sut.GetExampleRecords(new[] { "product name" });
                Assert.Equal(3, result.Items.Count);
            }
        }

        [Fact]
        public void Should_transform_underscores_to_spaces_when_required_headers_have_spaces3()
        {
            {
                var sut = CreateSut(new Dictionary<string, string> { { "product_name", "product" }, });
                var result = sut.GetExampleRecords(new[] { "product name" });
                Assert.Contains("product name", result.Header);
            }
        }

        [Fact]
        public void Should_transform_underscores_to_spaces_when_required_headers_have_spaces4()
        {
            {
                var sut = CreateSut(new Dictionary<string, string> { { "product_name", "product" }, });
                var result = sut.GetExampleRecords(new[] { "product name" });
                Assert.Equal("Chocolate", result.Items[0].Fields["product name"].Value);
            }
        }
    }
}
