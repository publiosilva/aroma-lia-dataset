public class TestURIHandlerService extends XTestCase {
    @Test
    public void testGetAuthorityWithScheme() throws Exception {
        URIHandlerService uriService = new URIHandlerService();
        URI uri = uriService.getAuthorityWithScheme("hdfs://nn1:8020/dataset/${YEAR}/${MONTH}");
        assertEquals("hdfs://nn1:8020", uri.toString());
        uri = uriService.getAuthorityWithScheme("hdfs://nn1:8020");
        assertEquals("hdfs://nn1:8020", uri.toString());
        uri = uriService.getAuthorityWithScheme("hdfs://nn1:8020/");
        assertEquals("hdfs://nn1:8020", uri.toString());
        uri = uriService.getAuthorityWithScheme("hdfs://///tmp/file");
        assertEquals("hdfs:///", uri.toString());
        uri = uriService.getAuthorityWithScheme("hdfs:///tmp/file");
        assertEquals("hdfs:///", uri.toString());
        uri = uriService.getAuthorityWithScheme("/tmp/file");
        assertEquals("/", uri.toString());
    }
}
