using Xunit;

namespace DefaultNamespace
{
    public class SpecificationProviderTests
    {
        [Fact]
        public void Should_collect_data_sets_from_tags()
        {
            {
                var sut = CreateSut();
                var result = sut.GetSpecification(new[] { new Tag(null, @"@DataSource:path\to\file.csv"), new Tag(null, @"@DataSet:data-set-name"), }, SOURCE_FILE_PATH);
                Assert.NotNull(result);
                Assert.NotNull(result.DataSet);
                Assert.Equal("data-set-name", result.DataSet);
            }
        }
    }
}
