using Xunit;

public class ImplementTriePrefixTree_208_test {

    [Fact]
    public void Check1() {
        var trie = new Trie();
        trie.Insert("hello");
        Assert.False(trie.Search("hell"), "Explanation message");
        Assert.False(trie.Search("helloa"), "Explanation message");
        Assert.True(trie.Search("hello"), "Explanation message");
        Assert.True(trie.StartsWith("hell"), "Explanation message");
        Assert.False(trie.StartsWith("helloa"), "Explanation message");
        Assert.True(trie.StartsWith("hello"), "Explanation message");
    }
}
