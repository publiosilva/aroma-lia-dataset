import pytest

class TestURIHandlerService:
    def test_get_authority_with_scheme(self):
        uri_service = URIHandlerService()
        uri = uri_service.get_authority_with_scheme("hdfs://nn1:8020/dataset/${YEAR}/${MONTH}")
        assert uri.to_string() == "hdfs://nn1:8020"
        uri = uri_service.get_authority_with_scheme("hdfs://nn1:8020")
        assert uri.to_string() == "hdfs://nn1:8020"
        uri = uri_service.get_authority_with_scheme("hdfs://nn1:8020/")
        assert uri.to_string() == "hdfs://nn1:8020"
        uri = uri_service.get_authority_with_scheme("hdfs://///tmp/file")
        assert uri.to_string() == "hdfs:///"
        uri = uri_service.get_authority_with_scheme("hdfs:///tmp/file")
        assert uri.to_string() == "hdfs:///"
        uri = uri_service.get_authority_with_scheme("/tmp/file")
        assert uri.to_string() == "/"
