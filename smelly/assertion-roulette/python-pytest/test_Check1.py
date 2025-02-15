import pytest

class TestImplementTriePrefixTree208:
    def test_check1(self):
        trie = Trie()
        trie.insert("hello")
        assert not trie.search("hell")
        assert not trie.search("helloa")
        assert trie.search("hello")
        assert trie.starts_with("hell")
        assert not trie.starts_with("helloa")
        assert trie.starts_with("hello")
