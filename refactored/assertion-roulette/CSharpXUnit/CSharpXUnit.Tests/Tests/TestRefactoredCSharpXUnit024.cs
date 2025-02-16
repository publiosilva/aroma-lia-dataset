using Xunit;

public class ValueFormatterTest
{
    [Fact]
    public void TestSerialisationOfRecursiveClassObjectToJsonIsSuccessful()
    {
        {
            RecursiveTest classObj = new RecursiveTest(0);
            StringBuilder builder = new StringBuilder();
            bool result = CreateValueFormatter().FormatValue(classObj, "", CaptureType.Serialize, null, builder);
            Assert.True(result, "Explanation message");
            string actual = builder.ToString();
            string deepestInteger = "\"Integer\":10";
            Assert.True(actual.Contains(deepestInteger), "Explanation message");
            string deepestNext = "\"Next\":\"NLog.UnitTests.MessageTemplates.ValueFormatterTest+RecursiveTest\"";
            Assert.True(actual.Contains(deepestNext), "Explanation message");
        }
    }
}
