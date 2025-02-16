using Xunit;

namespace DefaultNamespace
{
    public class CallSiteTests
    {
        [Fact(Skip = "")]
        public void Show_correct_method_with_async3()
        {
            {
                //namespace en name of current method
                const string currentMethodFullName = "NLog.UnitTests.LayoutRenderers.CallSiteTests.AsyncMethod3b";
                var logFactory = new LogFactory().Setup().LoadConfigurationFromXml(@"
                       <nlog>
                           <targets><target name='debug' type='Debug' layout='${callsite}|${message}' /></targets>
                           <rules>
                               <logger name='*' levels='Warn' writeTo='debug' />
                           </rules>
                       </nlog>").LogFactory;
                AsyncMethod3a(logFactory).Wait();
                logFactory.AssertDebugLastMessage($"{currentMethodFullName}|direct");
                new InnerClassAsyncMethod3().AsyncMethod3a(logFactory).Wait();
                logFactory.AssertDebugLastMessage($"{typeof(InnerClassAsyncMethod3).ToString()}.AsyncMethod3b|direct");
            }
        }
    }
}
