import pytest

class TestMasterClient:
    def test_open_close_1(self):
        master_client = MasterClient(mMasterInfo.get_master_address(), mExecutorService)
        assert not master_client.is_connected()
        master_client.connect()
        assert master_client.is_connected()
        master_client.user_create_file("/file", "", Constants.DEFAULT_BLOCK_SIZE_BYTE, True)
        assert master_client.get_file_status(-1, "/file") is not None

    def test_open_close_2(self):
        master_client = MasterClient(mMasterInfo.get_master_address(), mExecutorService)
        master_client.connect()
        master_client.user_create_file("/file", "", Constants.DEFAULT_BLOCK_SIZE_BYTE, True)
        master_client.close()
        assert not master_client.is_connected()
        master_client.connect()
        assert master_client.is_connected()
        assert master_client.get_file_status(-1, "/file") is not None
