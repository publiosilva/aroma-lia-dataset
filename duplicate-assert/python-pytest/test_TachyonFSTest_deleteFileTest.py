import pytest

class TestTachyonFS:
    def test_delete_file(self):
        uniq_path = TestUtils.uniq_path()
        workers = sTfs.get_workers_info()
        assert len(workers) == 1
        assert workers[0].get_capacity_bytes() == WORKER_CAPACITY_BYTES
        assert workers[0].get_used_bytes() == 0
        write_bytes = USER_QUOTA_UNIT_BYTES * 2

        # Delete non-existing files.
        assert sTfs.delete(TachyonURI(uniq_path), False)
        assert sTfs.delete(TachyonURI(uniq_path), True)

        for k in range(5):
            file_uri = TachyonURI(uniq_path + str(k))
            file_id = TestUtils.create_byte_file(sTfs, file_uri, WriteType.MUST_CACHE, write_bytes)
            file = sTfs.get_file(file_id)
            assert file.is_in_memory()
            assert sTfs.exist(file_uri)

            workers = sTfs.get_workers_info()
            assert len(workers) == 1
            assert workers[0].get_capacity_bytes() == WORKER_CAPACITY_BYTES
            assert workers[0].get_used_bytes() == write_bytes * (k + 1)

        for k in range(5):
            file_uri = TachyonURI(uniq_path + str(k))
            file_id = sTfs.get_file_id(file_uri)
            sTfs.delete(file_id, True)
            assert not sTfs.exist(file_uri)

            CommonUtils.sleep_ms(None, SLEEP_MS)
            workers = sTfs.get_workers_info()
            assert len(workers) == 1
            assert workers[0].get_capacity_bytes() == WORKER_CAPACITY_BYTES
            assert workers[0].get_used_bytes() == write_bytes * (4 - k)
