[Collection("EvoRunner")]
[EvoRunnerParameters(MockJVMNonDeterminism = true, UseVFS = true, UseVNET = true, ResetStaticState = true, SeparateClassLoader = true, UseJEE = true)]
public class GetRevision_ESTest : GetRevision_ESTest_scaffolding
{
    [Fact(Timeout = 4000)]
    public void Test03()
    {
        GetRevision getRevision0 = new GetRevision("no edit sum found found");
        HttpAction httpAction0 = getRevision0.GetNextMessage();
        Assert.Equal("/no edit sum found found?format=txt", httpAction0.Request, "Explanation message");

        HttpAction httpAction1 = getRevision0.GetNextMessage();
        Assert.NotNull(httpAction1, "Explanation message");
        Assert.Equal("/no edit sum found found", httpAction1.Request, "Explanation message");

        bool boolean0 = getRevision0.HasMoreMessages();
        Assert.True(boolean0, "Explanation message");
    }
}
