import pytest

class TestParameterVerifier:
    def test_verify_parameters_missing(self):
        conf = Configuration(False)
        
        str_ = "<root xmlns=\"uri:oozie:workflow:0.4\"><parameters>" \
                "<property><name>hello</name></property>" \
                "</parameters></root>"
        with pytest.raises(ParameterVerifierException) as excinfo:
            ParameterVerifier.verify_parameters(conf, XmlUtils.parse_xml(str_))
        assert excinfo.value.get_error_code() == ErrorCode.E0738
        assert excinfo.value.get_message().endswith("hello")
        assert "1" in excinfo.value.get_message()
        assert len(conf) == 0
        
        conf = Configuration(False)
        
        str_ = "<root xmlns=\"uri:oozie:workflow:0.4\"><parameters>" \
                "<property><name>hello</name><value>world</value></property>" \
                "</parameters></root>"
        ParameterVerifier.verify_parameters(conf, XmlUtils.parse_xml(str_))
        assert len(conf) == 1
        assert conf.get("hello") == "world"
        
        conf = Configuration(False)
        
        str_ = "<root xmlns=\"uri:oozie:workflow:0.4\"><parameters>" \
                "<property><name>hello</name></property>" \
                "<property><name>foo</name><value>bar</value></property>" \
                "<property><name>meh</name></property>" \
                "</parameters></root>"
        with pytest.raises(ParameterVerifierException) as excinfo:
            ParameterVerifier.verify_parameters(conf, XmlUtils.parse_xml(str_))
        assert excinfo.value.get_error_code() == ErrorCode.E0738
        assert excinfo.value.get_message().endswith("hello, meh")
        assert "foo" not in excinfo.value.get_message()
        assert "2" in excinfo.value.get_message()
        assert len(conf) == 1
        assert conf.get("foo") == "bar"
