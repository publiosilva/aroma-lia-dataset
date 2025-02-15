import pytest

class TestParameterVerifier:

    def test_verify_parameters_missing1(self):
        conf = Configuration(False)
        
        str_xml = "<root xmlns='uri:oozie:workflow:0.4'><parameters>" \
                  "<property><name>hello</name></property>" \
                  "</parameters></root>"
        with pytest.raises(ParameterVerifierException) as excinfo:
            ParameterVerifier.verify_parameters(conf, XmlUtils.parse_xml(str_xml))
        assert excinfo.value.get_error_code() == ErrorCode.E0738
        assert excinfo.value.message.endswith("hello")
        assert "1" in excinfo.value.message
        assert conf.size() == 0
        
        conf = Configuration(False)
        
        str_xml = "<root xmlns='uri:oozie:workflow:0.4'><parameters>" \
                  "<property><name>hello</name><value>world</value></property>" \
                  "</parameters></root>"
        ParameterVerifier.verify_parameters(conf, XmlUtils.parse_xml(str_xml))
        assert conf.size() == 1
        assert conf.get("hello") == "world"
        
        conf = Configuration(False)
        
        str_xml = "<root xmlns='uri:oozie:workflow:0.4'><parameters>" \
                  "<property><name>hello</name></property>" \
                  "<property><name>foo</name><value>bar</value></property>" \
                  "<property><name>meh</name></property>" \
                  "</parameters></root>"
        with pytest.raises(ParameterVerifierException) as excinfo:
            ParameterVerifier.verify_parameters(conf, XmlUtils.parse_xml(str_xml))
        assert excinfo.value.message.endswith("hello, meh")
        assert "foo" not in excinfo.value.message
        assert "2" in excinfo.value.message
        assert conf.get("foo") == "bar"

    def test_verify_parameters_missing2(self):
        conf = Configuration(False)
        
        str_xml = "<root xmlns='uri:oozie:workflow:0.4'><parameters>" \
                  "<property><name>hello</name></property>" \
                  "</parameters></root>"
        with pytest.raises(ParameterVerifierException) as excinfo:
            ParameterVerifier.verify_parameters(conf, XmlUtils.parse_xml(str_xml))
        assert excinfo.value.message.endswith("hello")
        assert "1" in excinfo.value.message
        assert conf.size() == 0
        
        conf = Configuration(False)
        
        str_xml = "<root xmlns='uri:oozie:workflow:0.4'><parameters>" \
                  "<property><name>hello</name><value>world</value></property>" \
                  "</parameters></root>"
        ParameterVerifier.verify_parameters(conf, XmlUtils.parse_xml(str_xml))
        assert conf.get("hello") == "world"
        
        conf = Configuration(False)
        
        str_xml = "<root xmlns='uri:oozie:workflow:0.4'><parameters>" \
                  "<property><name>hello</name></property>" \
                  "<property><name>foo</name><value>bar</value></property>" \
                  "<property><name>meh</name></property>" \
                  "</parameters></root>"
        with pytest.raises(ParameterVerifierException) as excinfo:
            ParameterVerifier.verify_parameters(conf, XmlUtils.parse_xml(str_xml))
        assert excinfo.value.get_error_code() == ErrorCode.E0738
        assert excinfo.value.message.endswith("hello, meh")
        assert "foo" not in excinfo.value.message
        assert "2" in excinfo.value.message
        assert conf.size() == 1
        assert conf.get("foo") == "bar"
