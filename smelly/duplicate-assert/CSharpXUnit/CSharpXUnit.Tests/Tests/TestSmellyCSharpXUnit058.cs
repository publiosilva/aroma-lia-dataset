public class TestURIHandlerService {
    [Fact]
    public void TestGetAuthorityWithScheme() {
        var uriService = new URIHandlerService();
        var uri = uriService.GetAuthorityWithScheme("hdfs://nn1:8020/dataset/${YEAR}/${MONTH}");
        Assert.Equal("hdfs://nn1:8020", uri.ToString());
        uri = uriService.GetAuthorityWithScheme("hdfs://nn1:8020");
        Assert.Equal("hdfs://nn1:8020", uri.ToString());
        uri = uriService.GetAuthorityWithScheme("hdfs://nn1:8020/");
        Assert.Equal("hdfs://nn1:8020", uri.ToString());
        uri = uriService.GetAuthorityWithScheme("hdfs://///tmp/file");
        Assert.Equal("hdfs:///", uri.ToString());
        uri = uriService.GetAuthorityWithScheme("hdfs:///tmp/file");
        Assert.Equal("hdfs:///", uri.ToString());
        uri = uriService.GetAuthorityWithScheme("/tmp/file");
        Assert.Equal("/", uri.ToString());
    }
}
