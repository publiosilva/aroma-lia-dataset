using Xunit;

public class ToDoApplicationTests {

    // [Fact(Skip = "")]
    [Fact]
    public void showCompletedExists() {
        // Arrange
        TodoList todo = new TodoList();
        // Act
        todo.SetShowCompleted(true);
    }
}
