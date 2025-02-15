using Xunit;

namespace DefaultNamespace
{
    public class StepExecutionTests
    {
        [Fact]
        public void ShouldCallBindingThatReturnsTask()
        {
            {
                var(testRunner, bindingMock) = GetTestRunnerFor<StepExecutionTestsBindings>();
                bool taskFinished = false;
                bindingMock.Setup(m => m.ReturnsATask()).Returns(Task.Factory.StartNew(() =>
                {
                    // Thread.Sleep(800);
                    taskFinished = true;
                }));
                //bindingInstance.Expect(b => b.ReturnsATask()).Return(Task.Factory.StartNew(() =>
                //    {
                //        Thread.Sleep(800);
                //        taskFinished = true;
                //    }));
                //MockRepository.ReplayAll();
                await testRunner.GivenAsync("Returns a Task");
                Assert.True(taskFinished);
            }
        }
    }
}
