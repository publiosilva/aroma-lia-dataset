using Xunit;

namespace DefaultNamespace
{
    public class TargetConfigurationTests
    {
        [Fact]
        public void WrapperRefTest1()
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
            }
        }

        [Fact]
        public void WrapperRefTest2()
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
                Assert.NotNull(c.FindTargetByName("b"));
            }
        }

        [Fact]
        public void WrapperRefTest3()
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
                Assert.NotNull(c.FindTargetByName("c"));
            }
        }

        [Fact]
        public void WrapperRefTest4()
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
                Assert.IsType<BufferingTargetWrapper>(c.FindTargetByName("b"));
            }
        }

        [Fact]
        public void WrapperRefTest5()
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
                Assert.IsType<AsyncTargetWrapper>(c.FindTargetByName("a"));
            }
        }

        [Fact]
        public void WrapperRefTest6()
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
                Assert.IsType<DebugTarget>(c.FindTargetByName("c"));
            }
        }

        [Fact]
        public void WrapperRefTest7()
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
                BufferingTargetWrapper btw = c.FindTargetByName("b") as BufferingTargetWrapper;
                AsyncTargetWrapper atw = c.FindTargetByName("a") as AsyncTargetWrapper;
                DebugTarget dt = c.FindTargetByName("c") as DebugTarget;
                Assert.Same(atw, btw.WrappedTarget);
            }
        }

        [Fact]
        public void WrapperRefTest8()
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
                BufferingTargetWrapper btw = c.FindTargetByName("b") as BufferingTargetWrapper;
                AsyncTargetWrapper atw = c.FindTargetByName("a") as AsyncTargetWrapper;
                DebugTarget dt = c.FindTargetByName("c") as DebugTarget;
                Assert.Same(dt, atw.WrappedTarget);
            }
        }

        [Fact]
        public void WrapperRefTest9()
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
                BufferingTargetWrapper btw = c.FindTargetByName("b") as BufferingTargetWrapper;
                AsyncTargetWrapper atw = c.FindTargetByName("a") as AsyncTargetWrapper;
                DebugTarget dt = c.FindTargetByName("c") as DebugTarget;
                Assert.Equal(19, btw.BufferSize);
            }
        }
    }
}
