import pytest

def test_open_close():
    master_client = MasterClient(mMasterInfo.getMasterAddress(), mExecutorService)
    assert not master_client.isConnected()
    master_client.connect()
    assert master_client.isConnected()
    master_client.user_createFile("/file", "", Constants.DEFAULT_BLOCK_SIZE_BYTE, True)
    assert master_client.getFileStatus(-1, "/file") is not None
    master_client.close()
    assert not master_client.isConnected()
    master_client.connect()
    assert master_client.isConnected()
    assert master_client.getFileStatus(-1, "/file") is not None
