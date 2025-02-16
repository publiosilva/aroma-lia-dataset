import pytest

class TestFileSystemNormalize:

    def test_fs_normalize(self):
        ScopeContext.push_property("foo", "abc.log")
        l = "${filesystem-normalize:${scopeproperty:foo}}"
        assert l.render(LogEventInfo.create_null_event()) == "abc.log"
        ScopeContext.push_property("foo", "")
        assert l.render(LogEventInfo.create_null_event()) == ""
        ScopeContext.push_property("foo", "a/b/c")
        assert l.render(LogEventInfo.create_null_event()) == "a_b_c"
        ScopeContext.push_property("foo", ":\\/$@#$%^")
        assert l.render(LogEventInfo.create_null_event()) == "_________"
