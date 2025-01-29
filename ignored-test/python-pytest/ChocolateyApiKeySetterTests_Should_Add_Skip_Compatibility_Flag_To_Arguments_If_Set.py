import pytest

class TestChocolateyApiKeySetter:

    @pytest.mark.skip(reason="")
    def test_should_add_skip_compatibility_flag_to_arguments_if_set(self):
        # Given
        fixture = ChocolateyApiKeySetterFixture()
        fixture.settings.skip_compatibility_checks = skip_compatibiity
        # When
        result = fixture.run()
        # Then
        assert result.args == expected
