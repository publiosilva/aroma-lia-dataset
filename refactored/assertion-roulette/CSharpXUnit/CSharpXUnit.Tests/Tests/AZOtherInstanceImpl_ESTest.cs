[Collection("EvoRunner")]
public class AZOtherInstanceImpl_ESTest : AZOtherInstanceImpl_ESTest_scaffolding
{
    [Fact(Timeout = 4000)]
    public void test44()
    {
        var inetAddress0 = System.Net.IPAddress.Loopback;
        var hashMap0 = new Dictionary<string, object>();
        var aZOtherInstanceImpl0 = new AZOtherInstanceImpl("so>]Ju~Kc2J5H@", "z@4}", inetAddress0, inetAddress0, 0, 121, 0, hashMap0);
        var boolean0 = aZOtherInstanceImpl0.Update(aZOtherInstanceImpl0);
        Assert.False(boolean0, "Explanation message");
        Assert.Equal(121, aZOtherInstanceImpl0.GetUDPListenPort(), "Explanation message");
        Assert.Equal(0, aZOtherInstanceImpl0.GetUDPNonDataListenPort(), "Explanation message");
        Assert.Equal(0, aZOtherInstanceImpl0.GetTCPListenPort(), "Explanation message");
    }
}
