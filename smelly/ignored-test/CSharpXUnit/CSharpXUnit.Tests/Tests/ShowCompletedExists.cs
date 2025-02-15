using Xunit;

namespace DefaultNamespace
{
    public class ToDoApplicationTests
    {
        [Fact(Skip = "")]
        public void ShowCompletedExists()
        {
            {
                // Arrange
                var todo = new TodoList();
                // Act
                todo.ShowCompleted = true;
            }
        }
    }
}
