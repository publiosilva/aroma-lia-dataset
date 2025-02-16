import pytest
import time

class TestAspMemoryCache:
    def test_should_clear_key_if_ttl_expired(self):
        fake = Fake(1)
        _cache.add("1", fake, 0.05, "region")
        # time.sleep(0.2)
        result = _cache.get("1", "region")
        assert result is None
