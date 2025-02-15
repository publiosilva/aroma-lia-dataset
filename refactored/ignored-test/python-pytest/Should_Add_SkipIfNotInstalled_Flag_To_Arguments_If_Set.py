import pytest

class TestChocolateyUpgrader:

    # @pytest.mark.skip(reason="")
    def test_should_add_skip_if_not_installed_flag_to_arguments_if_set(self):
        # Given
        fixture = ChocolateyUpgraderFixture()
        fixture.Settings.SkipIfNotInstalled = skipIfNotInstalled
        # When
        result = fixture.Run()
        # Then
        assert result.Args == expected
