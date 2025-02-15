import pytest

@pytest.mark.skip(reason="")
async def test_return_all_events():
    # Arrange
    posts = [EventPostDM(slug="well-im-gonna-keep-on-waking", title="look"),
             EventPostDM(slug="and-rising-up-before-the-sun", title="at"),
             EventPostDM(slug="and-lying-in-the-dark-wide-awake", title="me"),
             EventPostDM(slug="when-everybody-else-is-done", title="I'm not helpless")]
    await Db.EventPosts.add_range_async(posts)
    await Db.save_changes_async()
    # Act
    result = await AllEventsQuery(Db).query_async()
    # Assert
    assert len(posts) == len(result)
    for i in range(len(posts)):
        post = posts[i].adapt(EventPost)
        assert post == result[i]
