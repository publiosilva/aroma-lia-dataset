using Xunit;

namespace None
{
    public class ConditionEvaluatorTests
    {
        [Fact(Skip = "")]
        public void ExceptionTest14()
        {
            {
                var inner = new InvalidOperationException("f");
                var ex1 = new ConditionParseException("msg", inner);
                BinaryFormatter bf = new BinaryFormatter();
                MemoryStream ms = new MemoryStream();
                bf.Serialize(ms, ex1);
                ms.Position = 0;
                Exception ex2 = (Exception)bf.Deserialize(ms);
                Assert.Equal("msg", ex2.Message);
                Assert.Equal("f", ex2.InnerException.Message);
            }
        }
    }
}
