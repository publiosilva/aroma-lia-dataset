using Xunit;

public class GetRevision_ESTest : GetRevision_ESTest_scaffolding
{
    [Fact(Timeout = 4000)]
    public void Test031()
    {
        var getRevision0 = new GetRevision("no edit sum found found");
        var httpAction0 = getRevision0.GetNextMessage();
        Assert.Equal("/no edit sum found found?format=txt", httpAction0.GetRequest());
    }

    [Fact(Timeout = 4000)]
    public void Test032()
    {
        var getRevision0 = new GetRevision("no edit sum found found");
        var httpAction0 = getRevision0.GetNextMessage();
        
        var httpAction1 = getRevision0.GetNextMessage();
        Assert.NotNull(httpAction1);
    }

    [Fact(Timeout = 4000)]
    public void Test033()
    {
        var getRevision0 = new GetRevision("no edit sum found found");
        var httpAction0 = getRevision0.GetNextMessage();
        
        var httpAction1 = getRevision0.GetNextMessage();
        Assert.Equal("/no edit sum found found", httpAction1.GetRequest());
    }

    [Fact(Timeout = 4000)]
    public void Test034()
    {
        var getRevision0 = new GetRevision("no edit sum found found");
        var httpAction0 = getRevision0.GetNextMessage();
        
        var httpAction1 = getRevision0.GetNextMessage();
        
        var boolean0 = getRevision0.HasMoreMessages();
        Assert.True(boolean0);
    }
}
