using Xunit;

namespace DefaultNamespace
{
    public class StepExecutionTests
    {
        [Fact]
        public void ShouldCallBindingThatReturnsTaskAndReportError()
        {
            {
                var(testRunner, bindingMock) = GetTestRunnerFor<StepExecutionTestsBindings>();
                bool taskFinished = false;
                bindingMock.Setup(m => m.ReturnsATask()).Returns(Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(800);
                    taskFinished = true;
                    throw new Exception("catch meee");
                }));
                //bindingInstance.Expect(b => b.ReturnsATask()).Return(Task.Factory.StartNew(() =>
                //    {
                //        Thread.Sleep(800);
                //        taskFinished = true;
                //        throw new Exception("catch meee");
                //    }));
                //MockRepository.ReplayAll();
                await testRunner.GivenAsync("Returns a Task");
                Assert.True(taskFinished);
                GetLastTestStatus().Should().Be(ScenarioExecutionStatus.TestError);
                Assert.Equal("catch meee", ContextManagerStub.ScenarioContext.TestError.Message);
            }
        }
    }
}
