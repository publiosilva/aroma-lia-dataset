using Xunit;

public class TargetConfigurationTests
{
    [Fact]
    public void WrapperRefTest()
    {
        var c = XmlLoggingConfiguration.CreateFromXmlString(
                "<nlog>\n" +
                "    <targets>\n" +
                "        <target name='c' type='Debug' layout='${message}' />\n" +
                "        \n" +
                "        <wrapper name='a' type='AsyncWrapper'>\n" +
                "            <target-ref name='c' />\n" +
                "        </wrapper>\n" +
                "        \n" +
                "        <wrapper-target name='b' type='BufferingWrapper' bufferSize='19'>\n" +
                "            <wrapper-target-ref name='a' />\n" +
                "        </wrapper-target>\n" +
                "    </targets>\n" +
                "</nlog>");
        Assert.NotNull(c.FindTargetByName("a"), "Explanation message");
        Assert.NotNull(c.FindTargetByName("b"), "Explanation message");
        Assert.NotNull(c.FindTargetByName("c"), "Explanation message");
        Assert.IsType<BufferingTargetWrapper>(c.FindTargetByName("b"), "Explanation message");
        Assert.IsType<AsyncTargetWrapper>(c.FindTargetByName("a"), "Explanation message");
        Assert.IsType<DebugTarget>(c.FindTargetByName("c"), "Explanation message");
        var btw = (BufferingTargetWrapper)c.FindTargetByName("b");
        var atw = (AsyncTargetWrapper)c.FindTargetByName("a");
        var dt = (DebugTarget)c.FindTargetByName("c");
        Assert.Same(atw, btw.WrappedTarget, "Explanation message");
        Assert.Same(dt, atw.WrappedTarget, "Explanation message");
        Assert.Equal(19, btw.BufferSize, "Explanation message");
    }
}
