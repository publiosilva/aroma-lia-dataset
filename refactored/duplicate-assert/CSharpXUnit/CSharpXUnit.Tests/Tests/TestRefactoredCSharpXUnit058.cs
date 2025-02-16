public class TestURIHandlerService
{
    [Fact]
    public void TestGetAuthorityWithScheme1()
    {
        var uriService = new URIHandlerService();
        var uri = uriService.GetAuthorityWithScheme("hdfs://nn1:8020/dataset/${YEAR}/${MONTH}");
        Assert.Equal("hdfs://nn1:8020", uri.ToString());
    }

    [Fact]
    public void TestGetAuthorityWithScheme2()
    {
        var uriService = new URIHandlerService();
        var uri = uriService.GetAuthorityWithScheme("hdfs://nn1:8020/dataset/${YEAR}/${MONTH}");
        uri = uriService.GetAuthorityWithScheme("hdfs://nn1:8020");
        Assert.Equal("hdfs://nn1:8020", uri.ToString());
    }

    [Fact]
    public void TestGetAuthorityWithScheme3()
    {
        var uriService = new URIHandlerService();
        var uri = uriService.GetAuthorityWithScheme("hdfs://nn1:8020/dataset/${YEAR}/${MONTH}");
        uri = uriService.GetAuthorityWithScheme("hdfs://nn1:8020");
        uri = uriService.GetAuthorityWithScheme("hdfs://nn1:8020/");
        Assert.Equal("hdfs://nn1:8020", uri.ToString());
    }

    [Fact]
    public void TestGetAuthorityWithScheme4()
    {
        var uriService = new URIHandlerService();
        var uri = uriService.GetAuthorityWithScheme("hdfs://nn1:8020/dataset/${YEAR}/${MONTH}");
        uri = uriService.GetAuthorityWithScheme("hdfs://nn1:8020");
        uri = uriService.GetAuthorityWithScheme("hdfs://nn1:8020/");
        uri = uriService.GetAuthorityWithScheme("hdfs://///tmp/file");
        Assert.Equal("hdfs:///", uri.ToString());
    }

    [Fact]
    public void TestGetAuthorityWithScheme5()
    {
        var uriService = new URIHandlerService();
        var uri = uriService.GetAuthorityWithScheme("hdfs://nn1:8020/dataset/${YEAR}/${MONTH}");
        uri = uriService.GetAuthorityWithScheme("hdfs://nn1:8020");
        uri = uriService.GetAuthorityWithScheme("hdfs://nn1:8020/");
        uri = uriService.GetAuthorityWithScheme("hdfs://///tmp/file");
        uri = uriService.GetAuthorityWithScheme("hdfs:///tmp/file");
        Assert.Equal("hdfs:///", uri.ToString());
        uri = uriService.GetAuthorityWithScheme("/tmp/file");
        Assert.Equal("/", uri.ToString());
    }
}
