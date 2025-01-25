using Xunit;

namespace None
{
    public class CustomArgumentException
    {
        [Fact(Skip = "")]
        public void RecursiveAsyncExceptionWithoutFlattenException()
        {
            {
                var recursionCount = 3;
                Func<int> innerAction = () => throw new ApplicationException("Life is hard");
                var t1 = System.Threading.Tasks.Task<int>.Factory.StartNew(() =>
                {
                    return NestedFunc(recursionCount, innerAction);
                });
                try
                {
                    t1.Wait();
                }
                catch (AggregateException ex)
                {
                    var layoutRenderer = new ExceptionLayoutRenderer()
                    {
                        Format = "ToString",
                        FlattenException = false
                    };
                    var logEvent = LogEventInfo.Create(LogLevel.Error, null, null, (object)ex);
                    var result = layoutRenderer.Render(logEvent);
                    int needleCount = 0;
                    int foundIndex = result.IndexOf(nameof(NestedFunc), 0);
                    while (foundIndex >= 0)
                    {
                        ++needleCount;
                        foundIndex = result.IndexOf(nameof(NestedFunc), foundIndex + nameof(NestedFunc).Length);
                    }
            
                    Assert.True(needleCount >= recursionCount, $"{needleCount} too small");
                }
            }
        }
    }
}
