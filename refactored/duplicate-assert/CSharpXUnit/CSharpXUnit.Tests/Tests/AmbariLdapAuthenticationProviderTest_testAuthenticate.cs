public class AmbariLdapAuthenticationProviderTest
{
    [Fact]
    public void TestAuthenticate1()
    {
        Assert.Null(userDAO.FindLdapUserByName("allowedUser"), "User already exists in DB");
        var authentication = new UsernamePasswordAuthenticationToken("allowedUser", "password");
        var result = authenticationProvider.Authenticate(authentication);
        Assert.True(result.IsAuthenticated());
        Assert.NotNull(userDAO.FindLdapUserByName("allowedUser"), "User was not created");
    }

    [Fact]
    public void TestAuthenticate2()
    {
        Assert.Null(userDAO.FindLdapUserByName("allowedUser"), "User already exists in DB");
        var authentication = new UsernamePasswordAuthenticationToken("allowedUser", "password");
        var result = authenticationProvider.Authenticate(authentication);
        Assert.NotNull(userDAO.FindLdapUserByName("allowedUser"), "User was not created");
        result = authenticationProvider.Authenticate(authentication);
        Assert.True(result.IsAuthenticated());
    }
}
