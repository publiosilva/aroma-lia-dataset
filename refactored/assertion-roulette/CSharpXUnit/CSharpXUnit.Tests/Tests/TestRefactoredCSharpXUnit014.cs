[Collection("EvoRunner")]
[EvoRunnerParameters(MockJVMNonDeterminism = true, UseVFS = true, UseVNET = true, ResetStaticState = true, SeparateClassLoader = true, UseJEE = true)]
public class PollsChoiceModelImpl_ESTest : PollsChoiceModelImpl_ESTest_scaffolding
{
    [Fact(Timeout = 4000)]
    public void Test00()
    {
        var pollsChoiceImpl0 = new PollsChoiceImpl();
        var pollsChoiceWrapper0 = new PollsChoiceWrapper(pollsChoiceImpl0);
        pollsChoiceWrapper0.SetChoiceId(1L);
        bool boolean0 = pollsChoiceImpl0.Equals(pollsChoiceWrapper0);
        Assert.Equal(1L, pollsChoiceImpl0.GetChoiceId(), "Explanation message");
        Assert.True(boolean0, "Explanation message");
    }
}
