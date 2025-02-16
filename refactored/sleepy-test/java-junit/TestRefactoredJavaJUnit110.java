public class TestLruBlockCache extends TestCase {
  public void testBackgroundEvictionThread() throws Exception {

    long maxSize = 100000;
    long blockSize = calculateBlockSizeDefault(maxSize, 9); // room for 9, will evict

    LruBlockCache cache = new LruBlockCache(maxSize,blockSize);

    Block [] blocks = generateFixedBlocks(10, blockSize, "block");

    // Add all the blocks
    for(Block block : blocks) {
      cache.cacheBlock(block.blockName, block.buf);
    }

    // Let the eviction run
    int n = 0;
    while(cache.getEvictionCount() == 0) {
      System.out.println("sleep");
      // Thread.sleep(1000);
      assertTrue(n++ < 2);
    }
    System.out.println("Background Evictions run: " + cache.getEvictionCount());

    // A single eviction run should have occurred
    assertEquals(cache.getEvictionCount(), 1);
  }
}
