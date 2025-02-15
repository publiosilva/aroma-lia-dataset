using Xunit;

namespace DefaultNamespace
{
    public class TargetConfigurationTests
    {
        [Fact]
        public void WrapperRefTest()
        {
            {
                LoggingConfiguration c = XmlLoggingConfiguration.CreateFromXmlString(@"
                        <nlog>
                            <targets>
                                <target name='c' type='Debug' layout='${message}' />
            
                                <wrapper name='a' type='AsyncWrapper'>
                                    <target-ref name='c' />
                                </wrapper>
            
                                <wrapper-target name='b' type='BufferingWrapper' bufferSize='19'>
                                    <wrapper-target-ref name='a' />
                                </wrapper-target>
                            </targets>
                        </nlog>");
                Assert.NotNull(c.FindTargetByName("a"));
                Assert.NotNull(c.FindTargetByName("b"));
                Assert.NotNull(c.FindTargetByName("c"));
                Assert.IsType<BufferingTargetWrapper>(c.FindTargetByName("b"));
                Assert.IsType<AsyncTargetWrapper>(c.FindTargetByName("a"));
                Assert.IsType<DebugTarget>(c.FindTargetByName("c"));
                BufferingTargetWrapper btw = c.FindTargetByName("b") as BufferingTargetWrapper;
                AsyncTargetWrapper atw = c.FindTargetByName("a") as AsyncTargetWrapper;
                DebugTarget dt = c.FindTargetByName("c") as DebugTarget;
                Assert.Same(atw, btw.WrappedTarget);
                Assert.Same(dt, atw.WrappedTarget);
                Assert.Equal(19, btw.BufferSize);
            }
        }
    }
}
