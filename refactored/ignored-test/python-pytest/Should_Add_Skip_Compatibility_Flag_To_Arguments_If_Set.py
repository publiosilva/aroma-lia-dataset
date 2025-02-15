import pytest

class TestChocolateyUpgrader:

    # @pytest.mark.skip(reason="") 
    def test_should_add_skip_compatibility_flag_to_arguments_if_set(self):
        # Given
        fixture = ChocolateyUpgraderFixture()
        fixture.Settings.SkipCompatibilityChecks = skipCompatibiity
        # When
        result = fixture.Run()
        # Then
        assert expected == result.Args
