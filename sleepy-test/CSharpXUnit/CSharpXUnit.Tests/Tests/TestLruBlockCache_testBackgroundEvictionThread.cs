public class TestLruBlockCache
{
    [Fact]
    public void TestBackgroundEvictionThread()
    {
        long maxSize = 100000;
        long blockSize = CalculateBlockSizeDefault(maxSize, 9); // room for 9, will evict

        LruBlockCache cache = new LruBlockCache(maxSize, blockSize);

        Block[] blocks = GenerateFixedBlocks(10, blockSize, "block");

        // Add all the blocks
        foreach (Block block in blocks)
        {
            cache.CacheBlock(block.BlockName, block.Buf);
        }

        // Let the eviction run
        int n = 0;
        while (cache.GetEvictionCount() == 0)
        {
            Console.WriteLine("sleep");
            Thread.Sleep(1000);
            Assert.True(n++ < 2);
        }
        Console.WriteLine("Background Evictions run: " + cache.GetEvictionCount());

        // A single eviction run should have occurred
        Assert.Equal(cache.GetEvictionCount(), 1);
    }
}
