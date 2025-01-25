using Xunit;

public class DetailAstImplTest : AbstractModuleTestSupport
{
    [Fact]
    public void TestClearChildCountCache()
    {
        var parent = new DetailAstImpl();
        var child = new DetailAstImpl();
        parent.SetFirstChild(child);

        var clearChildCountCacheMethods = new List<Action<DetailAstImpl>>
        {
            child.SetNextSibling,
            child.AddPreviousSibling,
            child.AddNextSibling
        };

        foreach (var method in clearChildCountCacheMethods)
        {
            var startCount = parent.GetChildCount();
            method(null);
            var intermediateCount = (int)Whitebox.GetInternalState(parent, "childCount");
            var finishCount = parent.GetChildCount();
            Assert.Equal(startCount, finishCount, "Child count has changed");
            Assert.Equal(int.MinValue, intermediateCount, "Invalid child count");
        }

        var startCountChild = child.GetChildCount();
        child.AddChild(null);
        var intermediateCountChild = (int)Whitebox.GetInternalState(child, "childCount");
        var finishCountChild = child.GetChildCount();
        Assert.Equal(startCountChild, finishCountChild, "Child count has changed");
        Assert.Equal(int.MinValue, intermediateCountChild, "Invalid child count");
    }
}
