using Xunit;

namespace DefaultNamespace
{
    public class CallSiteTests
    {
        [Fact]
        public void CleanClassNamesOfAnonymousDelegatesTest()
        {
            {
                var logFactory = new LogFactory().Setup().LoadConfigurationFromXml(@"
                            <nlog>
                                <targets><target name='debug' type='Debug' layout='${callsite:ClassName=true:MethodName=false:CleanNamesOfAnonymousDelegates=true}' /></targets>
                                <rules>
                                    <logger name='*' levels='Fatal' writeTo='debug' />
                                </rules>
                            </nlog>").LogFactory;
                var logger = logFactory.GetLogger("A");
                bool done = false;
                ThreadPool.QueueUserWorkItem(state =>
                {
                    logger.Fatal("message");
                    done = true;
                }, null);
                while (done == false)
                {
                    // Thread.Sleep(10);
                }
            
                if (done == true)
                {
                    logFactory.AssertDebugLastMessage("NLog.UnitTests.LayoutRenderers.CallSiteTests");
                }
            }
        }
    }
}
