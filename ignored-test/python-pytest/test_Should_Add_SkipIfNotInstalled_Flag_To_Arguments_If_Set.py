import pytest

@pytest.mark.skip(reason="")
class TestChocolateyUpgrader:
    def test_should_add_skip_if_not_installed_flag_to_arguments_if_set(self):
        # Given
        fixture = ChocolateyUpgraderFixture()
        fixture.settings.skip_if_not_installed = skip_if_not_installed
        # When
        result = fixture.run()
        # Then
        assert expected == result.args
