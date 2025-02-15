import pytest
import asyncio

class TestStepExecution:
    @pytest.mark.asyncio
    async def test_should_call_binding_that_returns_task(self):
        test_runner, binding_mock = get_test_runner_for(StepExecutionTestsBindings)
        task_finished = False

        binding_mock.returns_a_task.return_value = asyncio.sleep(0.8, result=None).then(lambda: task_finished = True)

        await test_runner.given_async("Returns a Task")
        assert task_finished
