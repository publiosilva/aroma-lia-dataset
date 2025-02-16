[Collection("EvoRunner")]
public class PollsChoiceModelImpl_ESTest : PollsChoiceModelImpl_ESTest_scaffolding
{
    [Fact(Timeout = 4000)]
    public void test001()
    {
        var pollsChoiceImpl0 = new PollsChoiceImpl();
        var pollsChoiceWrapper0 = new PollsChoiceWrapper(pollsChoiceImpl0);
        pollsChoiceWrapper0.SetChoiceId(1L);
        var boolean0 = pollsChoiceImpl0.Equals(pollsChoiceWrapper0);
        Assert.Equal(1L, pollsChoiceImpl0.GetChoiceId());
    }

    [Fact(Timeout = 4000)]
    public void test002()
    {
        var pollsChoiceImpl0 = new PollsChoiceImpl();
        var pollsChoiceWrapper0 = new PollsChoiceWrapper(pollsChoiceImpl0);
        pollsChoiceWrapper0.SetChoiceId(1L);
        var boolean0 = pollsChoiceImpl0.Equals(pollsChoiceWrapper0);
        Assert.True(boolean0);
    }
}
