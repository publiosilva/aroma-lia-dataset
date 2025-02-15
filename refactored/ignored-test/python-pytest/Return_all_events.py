import pytest

class TestAllEventsQuery:

    def test_return_all_events(self):
        # Arrange
        posts = [
            EventPostDM("well-im-gonna-keep-on-waking", "look"),
            EventPostDM("and-rising-up-before-the-sun", "at"),
            EventPostDM("and-lying-in-the-dark-wide-awake", "me"),
            EventPostDM("when-everybody-else-is-done", "I'm not helpless")
        ]
        db.event_posts.extend(posts)
        db.save_changes()

        # Act
        result = AllEventsQuery(db).query_async()

        # Assert
        assert len(posts) == len(result)
        for i in range(len(posts)):
            post = posts[i].adapt(EventPost)
            assert post == result[i]
