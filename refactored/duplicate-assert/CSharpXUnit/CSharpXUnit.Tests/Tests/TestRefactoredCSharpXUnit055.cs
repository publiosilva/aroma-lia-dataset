public class TestParameterVerifier
{
    [Fact]
    public void TestVerifyParametersMissing1()
    {
        var conf = new Configuration(false);

        var str = "<root xmlns=\"uri:oozie:workflow:0.4\"><parameters>"
                + "<property><name>hello</name></property>"
                + "</parameters></root>";
        try
        {
            ParameterVerifier.VerifyParameters(conf, XmlUtils.ParseXml(str));
        }
        catch (ParameterVerifierException ex)
        {
            Assert.Equal(ErrorCode.E0738, ex.ErrorCode);
            Assert.EndsWith("hello", ex.Message);
            Assert.Contains("1", ex.Message);
            Assert.Equal(0, conf.Size());
        }

        conf = new Configuration(false);

        str = "<root xmlns=\"uri:oozie:workflow:0.4\"><parameters>"
                + "<property><name>hello</name><value>world</value></property>"
                + "</parameters></root>";
        ParameterVerifier.VerifyParameters(conf, XmlUtils.ParseXml(str));
        Assert.Equal(1, conf.Size());
        Assert.Equal("world", conf.Get("hello"));

        conf = new Configuration(false);

        str = "<root xmlns=\"uri:oozie:workflow:0.4\"><parameters>"
                + "<property><name>hello</name></property>"
                + "<property><name>foo</name><value>bar</value></property>"
                + "<property><name>meh</name></property>"
                + "</parameters></root>";
        try
        {
            ParameterVerifier.VerifyParameters(conf, XmlUtils.ParseXml(str));
            Assert.False(true);
        }
        catch (ParameterVerifierException ex)
        {
            Assert.EndsWith("hello, meh", ex.Message);
            Assert.DoesNotContain("foo", ex.Message);
            Assert.Contains("2", ex.Message);
            Assert.Equal("bar", conf.Get("foo"));
        }
    }

    [Fact]
    public void TestVerifyParametersMissing2()
    {
        var conf = new Configuration(false);

        var str = "<root xmlns=\"uri:oozie:workflow:0.4\"><parameters>"
                + "<property><name>hello</name></property>"
                + "</parameters></root>";
        try
        {
            ParameterVerifier.VerifyParameters(conf, XmlUtils.ParseXml(str));
        }
        catch (ParameterVerifierException ex)
        {
            Assert.EndsWith("hello", ex.Message);
            Assert.Contains("1", ex.Message);
            Assert.Equal(0, conf.Size());
        }

        conf = new Configuration(false);

        str = "<root xmlns=\"uri:oozie:workflow:0.4\"><parameters>"
                + "<property><name>hello</name><value>world</value></property>"
                + "</parameters></root>";
        ParameterVerifier.VerifyParameters(conf, XmlUtils.ParseXml(str));
        Assert.Equal("world", conf.Get("hello"));

        conf = new Configuration(false);

        str = "<root xmlns=\"uri:oozie:workflow:0.4\"><parameters>"
                + "<property><name>hello</name></property>"
                + "<property><name>foo</name><value>bar</value></property>"
                + "<property><name>meh</name></property>"
                + "</parameters></root>";
        try
        {
            ParameterVerifier.VerifyParameters(conf, XmlUtils.ParseXml(str));
            Assert.False(true);
        }
        catch (ParameterVerifierException ex)
        {
            Assert.Equal(ErrorCode.E0738, ex.ErrorCode);
            Assert.EndsWith("hello, meh", ex.Message);
            Assert.DoesNotContain("foo", ex.Message);
            Assert.Contains("2", ex.Message);
            Assert.Equal(1, conf.Size());
            Assert.Equal("bar", conf.Get("foo"));
        }
    }
}
