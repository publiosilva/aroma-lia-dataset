import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.assertEquals;

class AllEventsQuery_Should {

    @Test
    // @Disabled
    void return_all_events() throws Exception {
        // Arrange
        List<EventPostDM> posts = Arrays.asList(
            new EventPostDM("well-im-gonna-keep-on-waking", "look"),
            new EventPostDM("and-rising-up-before-the-sun", "at"),
            new EventPostDM("and-lying-in-the-dark-wide-awake", "me"),
            new EventPostDM("when-everybody-else-is-done", "I'm not helpless"));
        db.eventPosts.addAll(posts);
        db.saveChanges();

        // Act
        var result = new AllEventsQuery(db).queryAsync();

        // Assert
        assertEquals(posts.size(), result.size());
        for (int i = 0; i < posts.size(); i++) {
            var post = posts.get(i).adapt(EventPost.class);
            assertEquals(post, result.get(i));
        }
    }
}
