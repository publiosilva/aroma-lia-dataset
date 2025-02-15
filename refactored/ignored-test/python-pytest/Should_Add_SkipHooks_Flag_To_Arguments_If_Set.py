import pytest

class TestChocolateyUpgrader:

    # @pytest.mark.skip
    def test_should_add_skip_hooks_flag_to_arguments_if_set(self):
        # Given
        fixture = ChocolateyUpgraderFixture()
        fixture.settings.skip_hooks = skip_hooks
        # When
        result = fixture.run()
        # Then
        assert result.args == expected
