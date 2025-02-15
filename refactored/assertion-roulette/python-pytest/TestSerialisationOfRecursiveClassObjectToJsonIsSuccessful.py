import pytest

class TestValueFormatter:

    def test_serialisation_of_recursive_class_object_to_json_is_successful(self):
        class_obj = RecursiveTest(0)
        builder = []
        result = create_value_formatter().format_value(class_obj, "", CaptureType.Serialize, None, builder)
        assert result is True, "Explanation message"
        actual = ''.join(builder)
        deepest_integer = "\"Integer\":10"
        assert deepest_integer in actual, "Explanation message"
        deepest_next = "\"Next\":\"NLog.UnitTests.MessageTemplates.ValueFormatterTest+RecursiveTest\""
        assert deepest_next in actual, "Explanation message"
