import pytest

class TestValueFormatter:
    def test_serialisation_of_recursive_class_object_to_json_is_successful(self):
        @class_ = RecursiveTest(0)
        builder = StringIO()
        result = create_value_formatter().format_value(@class_, '', CaptureType.Serialize, None, builder)
        assert result
        actual = builder.getvalue()
        deepest_integer = '"Integer":10'
        assert deepest_integer in actual
        deepest_next = '"Next":"NLog.UnitTests.MessageTemplates.ValueFormatterTest+RecursiveTest"'
        assert deepest_next in actual
