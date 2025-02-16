[Collection("EvoRunner")]
[EvoRunnerParameters(MockJVMNonDeterminism = true, UseVFS = true, UseVNET = true, ResetStaticState = true, SeparateClassLoader = true, UseJEE = true)]
public class AZOtherInstanceImpl_ESTest : AZOtherInstanceImpl_ESTest_scaffolding 
{
    [Fact(Timeout = 4000)]
    public void Test441() 
    {
        var inetAddress0 = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList[0];
        var hashMap0 = new System.Collections.Generic.Dictionary<string, object>();
        var aZOtherInstanceImpl0 = new AZOtherInstanceImpl("so>]Ju~Kc2J5H@", "z@4}", inetAddress0, inetAddress0, 0, 121, 0, hashMap0);
        var boolean0 = aZOtherInstanceImpl0.Update(aZOtherInstanceImpl0);
        Assert.False(boolean0);
    }

    [Fact(Timeout = 4000)]
    public void Test442() 
    {
        var inetAddress0 = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList[0];
        var hashMap0 = new System.Collections.Generic.Dictionary<string, object>();
        var aZOtherInstanceImpl0 = new AZOtherInstanceImpl("so>]Ju~Kc2J5H@", "z@4}", inetAddress0, inetAddress0, 0, 121, 0, hashMap0);
        var boolean0 = aZOtherInstanceImpl0.Update(aZOtherInstanceImpl0);
        Assert.Equal(121, aZOtherInstanceImpl0.GetUDPListenPort());
    }

    [Fact(Timeout = 4000)]
    public void Test443() 
    {
        var inetAddress0 = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList[0];
        var hashMap0 = new System.Collections.Generic.Dictionary<string, object>();
        var aZOtherInstanceImpl0 = new AZOtherInstanceImpl("so>]Ju~Kc2J5H@", "z@4}", inetAddress0, inetAddress0, 0, 121, 0, hashMap0);
        var boolean0 = aZOtherInstanceImpl0.Update(aZOtherInstanceImpl0);
        Assert.Equal(0, aZOtherInstanceImpl0.GetUDPNonDataListenPort());
    }

    [Fact(Timeout = 4000)]
    public void Test444() 
    {
        var inetAddress0 = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList[0];
        var hashMap0 = new System.Collections.Generic.Dictionary<string, object>();
        var aZOtherInstanceImpl0 = new AZOtherInstanceImpl("so>]Ju~Kc2J5H@", "z@4}", inetAddress0, inetAddress0, 0, 121, 0, hashMap0);
        var boolean0 = aZOtherInstanceImpl0.Update(aZOtherInstanceImpl0);
        Assert.Equal(0, aZOtherInstanceImpl0.GetTCPListenPort());
    }
}
