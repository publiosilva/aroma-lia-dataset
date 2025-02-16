public class TestURIHandlerService {
    @Test
    public void testGetAuthorityWithScheme1() throws Exception {
        URIHandlerService uriService = new URIHandlerService();
        URI uri = uriService.getAuthorityWithScheme("hdfs://nn1:8020/dataset/${YEAR}/${MONTH}");
        assertEquals("hdfs://nn1:8020", uri.toString());
    }

    @Test
    public void testGetAuthorityWithScheme2() throws Exception {
        URIHandlerService uriService = new URIHandlerService();
        URI uri = uriService.getAuthorityWithScheme("hdfs://nn1:8020/dataset/${YEAR}/${MONTH}");
        uri = uriService.getAuthorityWithScheme("hdfs://nn1:8020");
        assertEquals("hdfs://nn1:8020", uri.toString());
    }

    @Test
    public void testGetAuthorityWithScheme3() throws Exception {
        URIHandlerService uriService = new URIHandlerService();
        URI uri = uriService.getAuthorityWithScheme("hdfs://nn1:8020/dataset/${YEAR}/${MONTH}");
        uri = uriService.getAuthorityWithScheme("hdfs://nn1:8020");
        uri = uriService.getAuthorityWithScheme("hdfs://nn1:8020/");
        assertEquals("hdfs://nn1:8020", uri.toString());
    }

    @Test
    public void testGetAuthorityWithScheme4() throws Exception {
        URIHandlerService uriService = new URIHandlerService();
        URI uri = uriService.getAuthorityWithScheme("hdfs://nn1:8020/dataset/${YEAR}/${MONTH}");
        uri = uriService.getAuthorityWithScheme("hdfs://nn1:8020");
        uri = uriService.getAuthorityWithScheme("hdfs://nn1:8020/");
        uri = uriService.getAuthorityWithScheme("hdfs://///tmp/file");
        assertEquals("hdfs:///", uri.toString());
    }

    @Test
    public void testGetAuthorityWithScheme5() throws Exception {
        URIHandlerService uriService = new URIHandlerService();
        URI uri = uriService.getAuthorityWithScheme("hdfs://nn1:8020/dataset/${YEAR}/${MONTH}");
        uri = uriService.getAuthorityWithScheme("hdfs://nn1:8020");
        uri = uriService.getAuthorityWithScheme("hdfs://nn1:8020/");
        uri = uriService.getAuthorityWithScheme("hdfs://///tmp/file");
        uri = uriService.getAuthorityWithScheme("hdfs:///tmp/file");
        assertEquals("hdfs:///", uri.toString());
        uri = uriService.getAuthorityWithScheme("/tmp/file");
        assertEquals("/", uri.toString());
    }
}
