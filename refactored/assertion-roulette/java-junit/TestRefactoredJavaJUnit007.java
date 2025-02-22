import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

class ImplementTriePrefixTree_208_test {

    @Test
    public void Check1() {
        Trie trie = new Trie();
        trie.insert("hello");
        assertFalse("Explanation message", trie.search("hell"));
        assertFalse("Explanation message", trie.search("helloa"));
        assertTrue("Explanation message", trie.search("hello"));
        assertTrue("Explanation message", trie.startsWith("hell"));
        assertFalse("Explanation message", trie.startsWith("helloa"));
        assertTrue("Explanation message", trie.startsWith("hello"));
    }
}
