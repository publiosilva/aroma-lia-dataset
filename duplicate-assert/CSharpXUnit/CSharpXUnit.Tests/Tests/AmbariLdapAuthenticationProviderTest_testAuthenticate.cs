public class AmbariLdapAuthenticationProviderTest
{
    [Fact]
    public void TestAuthenticate()
    {
        Assert.Null(userDAO.FindLdapUserByName("allowedUser"));
        var authentication = new UsernamePasswordAuthenticationToken("allowedUser", "password");
        var result = authenticationProvider.Authenticate(authentication);
        Assert.True(result.IsAuthenticated);
        Assert.NotNull(userDAO.FindLdapUserByName("allowedUser"));
        result = authenticationProvider.Authenticate(authentication);
        Assert.True(result.IsAuthenticated);
    }
}
