public class AmbariLdapAuthenticationProviderTest{
  @Test
  public void testAuthenticate1() throws Exception {
    assertNull("User alread exists in DB", userDAO.findLdapUserByName("allowedUser"));
    Authentication authentication = new UsernamePasswordAuthenticationToken("allowedUser", "password");
    Authentication result = authenticationProvider.authenticate(authentication);
    assertTrue(result.isAuthenticated());
    assertNotNull("User was not created", userDAO.findLdapUserByName("allowedUser"));
  }

  @Test
  public void testAuthenticate1() throws Exception {
    assertNull("User alread exists in DB", userDAO.findLdapUserByName("allowedUser"));
    Authentication authentication = new UsernamePasswordAuthenticationToken("allowedUser", "password");
    Authentication result = authenticationProvider.authenticate(authentication);
    assertNotNull("User was not created", userDAO.findLdapUserByName("allowedUser"));
    result = authenticationProvider.authenticate(authentication);
    assertTrue(result.isAuthenticated());
  }
}
