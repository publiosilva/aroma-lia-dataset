import org.junit.Test;
import static org.junit.Assert.assertTrue;
import static org.junit.Assert.assertEquals;

public class StepExecutionTests {
    
    @Test
    public void shouldCallBindingThatReturnsTaskAndReportError() throws Exception {
        var(testRunner, bindingMock) = getTestRunnerFor(StepExecutionTestsBindings.class);
        boolean taskFinished = false;
        bindingMock.when(m -> m.returnsATask()).thenReturn(CompletableFuture.supplyAsync(() -> {
            Thread.sleep(800);
            taskFinished = true;
            throw new RuntimeException("catch meee");
        }));
        await testRunner.givenAsync("Returns a Task");
        assertTrue(taskFinished);
        assertEquals(ScenarioExecutionStatus.TestError, getLastTestStatus());
        assertEquals("catch meee", ContextManagerStub.scenarioContext.testError.message);
    }
}
