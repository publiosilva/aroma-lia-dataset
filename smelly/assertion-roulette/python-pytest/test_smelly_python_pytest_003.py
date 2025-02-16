import pytest

class TestCursorPagingHandler:
    @pytest.mark.asyncio
    async def test_apply_pagination_before_last_10(self):
        # arrange
        data = [
            Foo.create(0),
            Foo.create(1),
            Foo.create(2),
            Foo.create(3),
        ]
        # act
        result = await apply(data, before=to_base64(3), last=10)
        # assert
        assert to_foo(result)[0].index == 0
        assert to_foo(result)[-1].index == 2
        assert result.info.has_next_page
        assert not result.info.has_previous_page
        assert result.info.start_cursor == to_base64(0)
        assert result.info.end_cursor == to_base64(2)
        assert await result.get_total_count_async() == 4
