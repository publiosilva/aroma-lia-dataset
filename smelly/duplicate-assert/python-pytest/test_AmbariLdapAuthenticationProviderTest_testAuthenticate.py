import pytest

class TestAmbariLdapAuthenticationProvider:
    def test_authenticate(self):
        assert user_dao.find_ldap_user_by_name("allowedUser") is None, "User already exists in DB"
        authentication = UsernamePasswordAuthenticationToken("allowedUser", "password")
        result = authentication_provider.authenticate(authentication)
        assert result.is_authenticated()
        assert user_dao.find_ldap_user_by_name("allowedUser") is not None, "User was not created"
        result = authentication_provider.authenticate(authentication)
        assert result.is_authenticated()
