using Xunit;

public class AllEventsQuery_Should {

    [Fact]
    // [Fact(Skip = "// @Disabled")]
    public async void Return_all_events() {
        // Arrange
        var posts = new List<EventPostDM> {
            new EventPostDM("well-im-gonna-keep-on-waking", "look"),
            new EventPostDM("and-rising-up-before-the-sun", "at"),
            new EventPostDM("and-lying-in-the-dark-wide-awake", "me"),
            new EventPostDM("when-everybody-else-is-done", "I'm not helpless")
        };
        db.eventPosts.AddRange(posts);
        await db.SaveChangesAsync();

        // Act
        var result = await new AllEventsQuery(db).QueryAsync();

        // Assert
        Assert.Equal(posts.Count, result.Count);
        for (int i = 0; i < posts.Count; i++) {
            var post = posts[i].Adapt<EventPost>();
            Assert.Equal(post, result[i]);
        }
    }
}
