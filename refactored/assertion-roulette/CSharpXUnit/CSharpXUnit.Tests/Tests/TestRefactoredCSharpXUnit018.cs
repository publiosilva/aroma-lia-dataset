using Xunit;

namespace DefaultNamespace
{
    public class SpecificationProviderTests
    {
        [Fact]
        public void Should_collect_data_sets_from_tags1()
        {
            {
                var sut = CreateSut();
                var result = sut.GetSpecification(new[] { new Tag(null, @"@DataSource:path\to\file.csv"), new Tag(null, @"@DataSet:data-set-name"), }, SOURCE_FILE_PATH);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public void Should_collect_data_sets_from_tags2()
        {
            {
                var sut = CreateSut();
                var result = sut.GetSpecification(new[] { new Tag(null, @"@DataSource:path\to\file.csv"), new Tag(null, @"@DataSet:data-set-name"), }, SOURCE_FILE_PATH);
                Assert.NotNull(result.DataSet);
            }
        }

        [Fact]
        public void Should_collect_data_sets_from_tags3()
        {
            {
                var sut = CreateSut();
                var result = sut.GetSpecification(new[] { new Tag(null, @"@DataSource:path\to\file.csv"), new Tag(null, @"@DataSet:data-set-name"), }, SOURCE_FILE_PATH);
                Assert.Equal("data-set-name", result.DataSet);
            }
        }
    }
}
