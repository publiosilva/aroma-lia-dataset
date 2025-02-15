import pytest

class TestFileSystemNormalize:

    def test_fs_normalize_1(self):
        ScopeContext.push_property("foo", "abc.log")
        l = "${filesystem-normalize:${scopeproperty:foo}}"
        assert l.render(LogEventInfo.create_null_event()) == "abc.log", "Explanation message"
        ScopeContext.push_property("foo", "")
        assert l.render(LogEventInfo.create_null_event()) == "", "Explanation message"
        ScopeContext.push_property("foo", "a/b/c")
        assert l.render(LogEventInfo.create_null_event()) == "a_b_c", "Explanation message"
        ScopeContext.push_property("foo", ":\\/$@#$%^")
        assert l.render(LogEventInfo.create_null_event()) == "_________", "Explanation message"
