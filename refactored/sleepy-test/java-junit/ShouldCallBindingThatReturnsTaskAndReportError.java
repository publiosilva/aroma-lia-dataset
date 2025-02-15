import org.junit.Test;
import static org.junit.Assert.assertTrue;
import static org.junit.Assert.assertEquals;

public class StepExecutionTests {
    
    @Test
    public void shouldCallBindingThatReturnsTaskAndReportError() throws Exception {
        Object[] result = getTestRunnerFor(StepExecutionTestsBindings.class);
        TestRunner testRunner = (TestRunner) result[0];
        StepExecutionTestsBindings bindingMock = (StepExecutionTestsBindings) result[1];
        boolean taskFinished = false;
        bindingMock.when(m -> m.returnsATask()).thenReturn(CompletableFuture.supplyAsync(() -> {
            // Thread.sleep(800);
            taskFinished = true;
            throw new RuntimeException("catch meee");
        }));
        testRunner.givenAsync("Returns a Task");
        assertTrue(taskFinished);
        assertEquals(ScenarioExecutionStatus.TestError, getLastTestStatus());
        assertEquals("catch meee", ContextManagerStub.scenarioContext.testError.message);
    }
}
