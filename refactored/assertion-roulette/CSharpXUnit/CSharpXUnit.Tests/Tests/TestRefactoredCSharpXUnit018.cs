using Xunit;

public class SpecificationProviderTests
{
    [Fact]
    public void ShouldCollectDataSetsFromTags()
    {
        {
            var sut = CreateSut();
            var result = sut.GetSpecification(new Tag[] { new Tag(null, "@DataSource:path\\to\\file.csv"), new Tag(null, "@DataSet:data-set-name") }, SOURCE_FILE_PATH);
            Assert.NotNull("Explanation message", result);
            Assert.NotNull("Explanation message", result.DataSet);
            Assert.Equal("Explanation message", "data-set-name", result.DataSet);
        }
    }
}
