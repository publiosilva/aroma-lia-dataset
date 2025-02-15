import pytest

class TestAmbariLdapAuthenticationProvider:
    def test_authenticate_1(self):
        assert userDAO.findLdapUserByName("allowedUser") is None, "User already exists in DB"
        authentication = UsernamePasswordAuthenticationToken("allowedUser", "password")
        result = authenticationProvider.authenticate(authentication)
        assert result.isAuthenticated() is True
        assert userDAO.findLdapUserByName("allowedUser") is not None, "User was not created"

    def test_authenticate_2(self):
        assert userDAO.findLdapUserByName("allowedUser") is None, "User already exists in DB"
        authentication = UsernamePasswordAuthenticationToken("allowedUser", "password")
        result = authenticationProvider.authenticate(authentication)
        assert userDAO.findLdapUserByName("allowedUser") is not None, "User was not created"
        result = authenticationProvider.authenticate(authentication)
        assert result.isAuthenticated() is True
