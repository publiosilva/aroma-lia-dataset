import pytest

class TestChocolateyPusher:

    # @pytest.mark.skip(reason=" ")
    def test_should_add_skip_cleanup_flag_to_arguments_if_set(self):
        # Given
        fixture = ChocolateyPusherFixture()
        fixture.get_settings().set_skip_cleanup(skip_cleanup)
        # When
        result = fixture.run()
        # Then
        assert expected == result.get_args()
