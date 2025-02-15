import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.assertTrue;

class StepExecutionTests {

    @Test
    void shouldCallBindingThatReturnsTask() throws Exception {
        var testRunnerandBindingMock = getTestRunnerFor(StepExecutionTestsBindings.class);
        boolean[] taskFinished = {false};
        bindingMock.setup(m -> m.returnsATask()).returns(CompletableFuture.runAsync(() -> {
            try {
                Thread.sleep(800);
                taskFinished[0] = true;
            } catch (InterruptedException e) {
                Thread.currentThread().interrupt();
            }
        }));
        await testRunner.givenAsync("Returns a Task");
        assertTrue(taskFinished[0]);
    }
}
