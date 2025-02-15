import pytest

class TestURIHandlerService:
    def test_get_authority_with_scheme_1(self):
        uri_service = URIHandlerService()
        uri = uri_service.get_authority_with_scheme("hdfs://nn1:8020/dataset/${YEAR}/${MONTH}")
        assert uri.to_string() == "hdfs://nn1:8020"

    def test_get_authority_with_scheme_2(self):
        uri_service = URIHandlerService()
        uri = uri_service.get_authority_with_scheme("hdfs://nn1:8020/dataset/${YEAR}/${MONTH}")
        uri = uri_service.get_authority_with_scheme("hdfs://nn1:8020")
        assert uri.to_string() == "hdfs://nn1:8020"

    def test_get_authority_with_scheme_3(self):
        uri_service = URIHandlerService()
        uri = uri_service.get_authority_with_scheme("hdfs://nn1:8020/dataset/${YEAR}/${MONTH}")
        uri = uri_service.get_authority_with_scheme("hdfs://nn1:8020")
        uri = uri_service.get_authority_with_scheme("hdfs://nn1:8020/")
        assert uri.to_string() == "hdfs://nn1:8020"

    def test_get_authority_with_scheme_4(self):
        uri_service = URIHandlerService()
        uri = uri_service.get_authority_with_scheme("hdfs://nn1:8020/dataset/${YEAR}/${MONTH}")
        uri = uri_service.get_authority_with_scheme("hdfs://nn1:8020")
        uri = uri_service.get_authority_with_scheme("hdfs://nn1:8020/")
        uri = uri_service.get_authority_with_scheme("hdfs://///tmp/file")
        assert uri.to_string() == "hdfs:///"

    def test_get_authority_with_scheme_5(self):
        uri_service = URIHandlerService()
        uri = uri_service.get_authority_with_scheme("hdfs://nn1:8020/dataset/${YEAR}/${MONTH}")
        uri = uri_service.get_authority_with_scheme("hdfs://nn1:8020")
        uri = uri_service.get_authority_with_scheme("hdfs://nn1:8020/")
        uri = uri_service.get_authority_with_scheme("hdfs://///tmp/file")
        uri = uri_service.get_authority_with_scheme("hdfs:///tmp/file")
        assert uri.to_string() == "hdfs:///"
        uri = uri_service.get_authority_with_scheme("/tmp/file")
        assert uri.to_string() == "/"
