import pytest

@pytest.mark.skip(reason="")
class TestChocolateyUpgrader:

    def test_should_add_skip_compatibility_flag_to_arguments_if_set(self):
        # Given
        fixture = ChocolateyUpgraderFixture()
        fixture.settings.skip_compatibility_checks = skip_compatibility
        # When
        result = fixture.run()
        # Then
        assert expected == result.args
