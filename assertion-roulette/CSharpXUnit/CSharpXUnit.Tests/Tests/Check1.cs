using Xunit;

namespace DefaultNamespace
{
    public class ImplementTriePrefixTree_208_test
    {
        [Fact]
        public void Check1()
        {
            {
                var trie = new Trie();
                trie.Insert("hello");
                Assert.False(trie.Search("hell"));
                Assert.False(trie.Search("helloa"));
                Assert.True(trie.Search("hello"));
                Assert.True(trie.StartsWith("hell"));
                Assert.False(trie.StartsWith("helloa"));
                Assert.True(trie.StartsWith("hello"));
            }
        }
    }
}
