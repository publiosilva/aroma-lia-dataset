import pytest

class TestTachyonFS:
    
    @pytest.mark.parametrize("test_case", range(1, 4))
    def test_delete_file(self, test_case):
        uniq_path = TestUtils.uniqPath()
        workers = sTfs.getWorkersInfo()
        assert len(workers) == 1
        assert workers[0].getUsedBytes() == 0
        write_bytes = USER_QUOTA_UNIT_BYTES * 2

        assert sTfs.delete(TachyonURI(uniq_path), False)
        assert sTfs.delete(TachyonURI(uniq_path), True)

        for k in range(5):
            file_uri = TachyonURI(uniq_path + str(k))
            file_id = TestUtils.createByteFile(sTfs, file_uri, WriteType.MUST_CACHE, write_bytes)
            file = sTfs.getFile(file_id)
            assert file.isInMemory()
            assert sTfs.exist(file_uri)

            workers = sTfs.getWorkersInfo()
            assert write_bytes * (k + 1) == workers[0].getUsedBytes()

        for k in range(5):
            file_uri = TachyonURI(uniq_path + str(k))
            file_id = sTfs.getFileId(file_uri)
            sTfs.delete(file_id, True)
            assert not sTfs.exist(file_uri)

            CommonUtils.sleepMs(None, SLEEP_MS)
            workers = sTfs.getWorkersInfo()
            assert write_bytes * (4 - k) == workers[0].getUsedBytes()
