import pytest

# @pytest.mark.skip
class TestLayoutTyped:

    def test_render_should_recognize_stack_trace_usage(self):
        # Arrange
        callback_args = None
        
        def callback(evt, args):
            nonlocal callback_args
            callback_args = args
        
        logger = LogFactory().setup().load_configuration(lambda builder: (
            builder.for_logger().write_to(MethodCallTarget("dbg", callback).add_parameter("LineNumber", "${callsite-linenumber}", int))
        )).get_logger("renderShouldRecognizeStackTraceUsage")
        
        # Act
        logger.info("Testing")
        
        # Assert
        assert len(callback_args) == 1
        line_number = callback_args[0]
        assert line_number > 0
