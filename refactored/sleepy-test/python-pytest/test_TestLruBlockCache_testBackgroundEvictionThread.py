import pytest

class TestLruBlockCache:

    def test_background_eviction_thread(self):
        max_size = 100000
        block_size = calculate_block_size_default(max_size, 9)  # room for 9, will evict

        cache = LruBlockCache(max_size, block_size)

        blocks = generate_fixed_blocks(10, block_size, "block")

        # Add all the blocks
        for block in blocks:
            cache.cache_block(block.block_name, block.buf)

        # Let the eviction run
        n = 0
        while cache.get_eviction_count() == 0:
            print("sleep")
            # time.sleep(1)
            assert n < 2
            n += 1
        print(f"Background Evictions run: {cache.get_eviction_count()}")

        # A single eviction run should have occurred
        assert cache.get_eviction_count() == 1
