import org.junit.Test;
import org.junit.Ignore;

public class ToDoApplicationTests {

    @Ignore("")
    @Test
    public void showCompletedExists() {
        // Arrange
        TodoList todo = new TodoList();
        // Act
        todo.setShowCompleted(true);
    }
}
