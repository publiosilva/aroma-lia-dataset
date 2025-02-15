using Xunit;

namespace DefaultNamespace
{
    public class CallSiteTests
    {
        [Fact(Skip = "")]
        public void LogAfterAwait_ShouldCleanMethodNameAsync5()
        {
            {
                //namespace en name of current method
                const string currentMethodFullName = "NLog.UnitTests.LayoutRenderers.CallSiteTests.AsyncMethod5";
                var logFactory = new LogFactory().Setup().LoadConfigurationFromXml(@"
                       <nlog>
                           <targets><target name='debug' type='Debug' layout='${callsite}|${message}' /></targets>
                           <rules>
                               <logger name='*' levels='Debug' writeTo='debug' />
                           </rules>
                       </nlog>").LogFactory;
                AsyncMethod5(logFactory).Wait();
                logFactory.AssertDebugLastMessage($"{currentMethodFullName}|dude");
                new InnerClassAsyncMethod5().AsyncMethod5(logFactory).Wait();
                logFactory.AssertDebugLastMessage($"{typeof(InnerClassAsyncMethod5).ToString()}.AsyncMethod5|dude");
            }
        }
    }
}
