import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

class ImplementTriePrefixTree_208_test {

    @Test
    public void Check1() {
        Trie trie = new Trie();
        trie.insert("hello");
        assertFalse(trie.search("hell"));
        assertFalse(trie.search("helloa"));
        assertTrue(trie.search("hello"));
        assertTrue(trie.startsWith("hell"));
        assertFalse(trie.startsWith("helloa"));
        assertTrue(trie.startsWith("hello"));
    }
}
