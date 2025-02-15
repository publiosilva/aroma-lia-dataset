using Xunit;

namespace DefaultNamespace
{
    public class FileSystemNormalizeTests
    {
        [Fact]
        public void FSNormalizeTest1()
        {
            {
                ScopeContext.PushProperty("foo", "abc.log");
                SimpleLayout l = "${filesystem-normalize:${scopeproperty:foo}}";
                Assert.Equal("abc.log", l.Render(LogEventInfo.CreateNullEvent()));
                ScopeContext.PushProperty("foo", "");
                Assert.Equal("", l.Render(LogEventInfo.CreateNullEvent()));
                ScopeContext.PushProperty("foo", "a/b/c");
                Assert.Equal("a_b_c", l.Render(LogEventInfo.CreateNullEvent()));
                // all characters outside of alpha/digits/space/_/./- are replaced with _
                ScopeContext.PushProperty("foo", ":\\/$@#$%^");
                Assert.Equal("_________", l.Render(LogEventInfo.CreateNullEvent()));
            }
        }
    }
}
