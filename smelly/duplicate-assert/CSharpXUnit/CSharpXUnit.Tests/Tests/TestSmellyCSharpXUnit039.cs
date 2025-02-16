[Collection("Evo")]
public class HookHotDeployListener_ESTest : HookHotDeployListener_ESTest_scaffolding
{
    [Fact(Timeout = 4000)]
    public void Test00()
    {
        var hookHotDeployListener0 = new HookHotDeployListener();
        Assert.NotNull(hookHotDeployListener0);
        
        var mockServletContext0 = new MockServletContext();
        Assert.NotNull(mockServletContext0);
        Assert.Null(mockServletContext0.GetServerInfo());
        Assert.Equal(0, mockServletContext0.GetMajorVersion());
        Assert.Equal(0, mockServletContext0.GetMinorVersion());
        Assert.Null(mockServletContext0.GetServletContextName());
        
        var hotDeployEvent0 = new HotDeployEvent(mockServletContext0, null);
        Assert.NotNull(hotDeployEvent0);
        Assert.Null(mockServletContext0.GetServerInfo());
        Assert.Equal(0, mockServletContext0.GetMajorVersion());
        Assert.Equal(0, mockServletContext0.GetMinorVersion());
        Assert.Null(mockServletContext0.GetServletContextName());
        
        hookHotDeployListener0.DoInvokeUndeploy(hotDeployEvent0);
        Assert.Null(mockServletContext0.GetServerInfo());
        Assert.Equal(0, mockServletContext0.GetMajorVersion());
        Assert.Equal(0, mockServletContext0.GetMinorVersion());
        Assert.Null(mockServletContext0.GetServletContextName());
    }
}
