using Xunit;

namespace DefaultNamespace
{
    public class ValueFormatterTest
    {
        [Fact]
        public void TestSerialisationOfRecursiveClassObjectToJsonIsSuccessful1()
        {
            {
                var @class = new RecursiveTest(0);
                StringBuilder builder = new StringBuilder();
                var result = CreateValueFormatter().FormatValue(@class, string.Empty, CaptureType.Serialize, null, builder);
                Assert.True(result);
            }
        }

        [Fact]
        public void TestSerialisationOfRecursiveClassObjectToJsonIsSuccessful2()
        {
            {
                var @class = new RecursiveTest(0);
                StringBuilder builder = new StringBuilder();
                var result = CreateValueFormatter().FormatValue(@class, string.Empty, CaptureType.Serialize, null, builder);
                var actual = builder.ToString();
                var deepestInteger = @"""Integer"":10";
                Assert.Contains(deepestInteger, actual);
            }
        }

        [Fact]
        public void TestSerialisationOfRecursiveClassObjectToJsonIsSuccessful3()
        {
            {
                var @class = new RecursiveTest(0);
                StringBuilder builder = new StringBuilder();
                var result = CreateValueFormatter().FormatValue(@class, string.Empty, CaptureType.Serialize, null, builder);
                var actual = builder.ToString();
                var deepestInteger = @"""Integer"":10";
                var deepestNext = @"""Next"":""NLog.UnitTests.MessageTemplates.ValueFormatterTest+RecursiveTest""";
                Assert.Contains(deepestNext, actual);
            }
        }
    }
}
