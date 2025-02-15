import pytest

class TestLayoutTyped:
    
    @pytest.mark.skip(reason="")
    def test_render_should_recognize_stack_trace_usage(self):
        # Arrange
        callback_args = None
        callback = lambda evt, args: callback_args.__setstate__(args)
        logger = LogFactory().setup().load_configuration(lambda builder: {
            method_call = NLog.Targets.MethodCallTarget("dbg", callback)
            method_call.parameters.add(NLog.Targets.MethodCallParameter("LineNumber", "${callsite-linenumber}", int))
            builder.for_logger().write_to(method_call)
        }).get_logger("test_render_should_recognize_stack_trace_usage")
        
        # Act
        logger.info("Testing")
        
        # Assert
        assert len(callback_args) == 1
        line_number = callback_args[0]
        assert isinstance(line_number, int)
        assert line_number > 0
