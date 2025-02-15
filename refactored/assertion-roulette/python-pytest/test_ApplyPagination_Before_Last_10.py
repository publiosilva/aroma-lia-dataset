import pytest

class TestCursorPagingHandler:

    def test_apply_pagination_before_last_10(self):
        # arrange
        data = [
            Foo.create(0),
            Foo.create(1),
            Foo.create(2),
            Foo.create(3),
        ]
        # act
        result = apply(data, to_base64(3), 10)
        # assert
        assert to_foo(result)[0].get_index() == 0, "Explanation message"
        assert to_foo(result)[-1].get_index() == 2, "Explanation message"
        assert result.get_info().has_next_page(), "Explanation message"
        assert not result.get_info().has_previous_page(), "Explanation message"
        assert result.get_info().get_start_cursor() == to_base64(0), "Explanation message"
        assert result.get_info().get_end_cursor() == to_base64(2), "Explanation message"
        assert result.get_total_count_async() == 4, "Explanation message"
