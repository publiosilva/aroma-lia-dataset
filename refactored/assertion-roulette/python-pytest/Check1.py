import pytest

class TestImplementTriePrefixTree:

    def test_check1(self):
        trie = Trie()
        trie.insert("hello")
        assert not trie.search("hell"), "Explanation message"
        assert not trie.search("helloa"), "Explanation message"
        assert trie.search("hello"), "Explanation message"
        assert trie.startsWith("hell"), "Explanation message"
        assert not trie.startsWith("helloa"), "Explanation message"
        assert trie.startsWith("hello"), "Explanation message"
