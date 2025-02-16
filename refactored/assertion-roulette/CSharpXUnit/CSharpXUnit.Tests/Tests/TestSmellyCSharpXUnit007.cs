using Xunit;

namespace DefaultNamespace
{
    public class ImplementTriePrefixTree_208_test
    {
        [Fact]
        public void Check11()
        {
            {
                var trie = new Trie();
                trie.Insert("hello");
                Assert.False(trie.Search("hell"));
            }
        }

        [Fact]
        public void Check12()
        {
            {
                var trie = new Trie();
                trie.Insert("hello");
                Assert.False(trie.Search("helloa"));
            }
        }

        [Fact]
        public void Check13()
        {
            {
                var trie = new Trie();
                trie.Insert("hello");
                Assert.True(trie.Search("hello"));
            }
        }

        [Fact]
        public void Check14()
        {
            {
                var trie = new Trie();
                trie.Insert("hello");
                Assert.True(trie.StartsWith("hell"));
            }
        }

        [Fact]
        public void Check15()
        {
            {
                var trie = new Trie();
                trie.Insert("hello");
                Assert.False(trie.StartsWith("helloa"));
            }
        }

        [Fact]
        public void Check16()
        {
            {
                var trie = new Trie();
                trie.Insert("hello");
                Assert.True(trie.StartsWith("hello"));
            }
        }
    }
}
