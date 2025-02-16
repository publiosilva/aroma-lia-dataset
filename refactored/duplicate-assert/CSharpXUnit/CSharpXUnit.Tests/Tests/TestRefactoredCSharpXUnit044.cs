public class TachyonFSTest
{
    [Fact]
    public void DeleteFileTest1()
    {
        string uniqPath = TestUtils.UniqPath();
        List<ClientWorkerInfo> workers = sTfs.GetWorkersInfo();
        Assert.Equal(1, workers.Count);
        Assert.Equal(WORKER_CAPACITY_BYTES, workers[0].GetCapacityBytes());
        Assert.Equal(0, workers[0].GetUsedBytes());
        int writeBytes = USER_QUOTA_UNIT_BYTES * 2;

        // Delete non-existing files.
        Assert.True(sTfs.Delete(new TachyonURI(uniqPath), false));
        Assert.True(sTfs.Delete(new TachyonURI(uniqPath), true));

        for (int k = 0; k < 5; k++)
        {
            TachyonURI fileURI = new TachyonURI(uniqPath + k);
            int fileId = TestUtils.CreateByteFile(sTfs, fileURI, WriteType.MUST_CACHE, writeBytes);
            TachyonFile file = sTfs.GetFile(fileId);
            Assert.True(file.IsInMemory());
            Assert.True(sTfs.Exist(fileURI));

            workers = sTfs.GetWorkersInfo();
            Assert.Equal(writeBytes * (k + 1), workers[0].GetUsedBytes());
        }

        for (int k = 0; k < 5; k++)
        {
            TachyonURI fileURI = new TachyonURI(uniqPath + k);
            int fileId = sTfs.GetFileId(fileURI);
            sTfs.Delete(fileId, true);
            Assert.False(sTfs.Exist(fileURI));

            CommonUtils.SleepMs(null, SLEEP_MS);
            workers = sTfs.GetWorkersInfo();
            Assert.Equal(writeBytes * (4 - k), workers[0].GetUsedBytes());
        }
    }

    [Fact]
    public void DeleteFileTest2()
    {
        string uniqPath = TestUtils.UniqPath();
        List<ClientWorkerInfo> workers = sTfs.GetWorkersInfo();
        Assert.Equal(0, workers[0].GetUsedBytes());
        int writeBytes = USER_QUOTA_UNIT_BYTES * 2;

        // Delete non-existing files.
        Assert.True(sTfs.Delete(new TachyonURI(uniqPath), false));
        Assert.True(sTfs.Delete(new TachyonURI(uniqPath), true));

        for (int k = 0; k < 5; k++)
        {
            TachyonURI fileURI = new TachyonURI(uniqPath + k);
            int fileId = TestUtils.CreateByteFile(sTfs, fileURI, WriteType.MUST_CACHE, writeBytes);
            TachyonFile file = sTfs.GetFile(fileId);
            Assert.True(file.IsInMemory());
            Assert.True(sTfs.Exist(fileURI));

            workers = sTfs.GetWorkersInfo();
            Assert.Equal(1, workers.Count);
            Assert.Equal(WORKER_CAPACITY_BYTES, workers[0].GetCapacityBytes());
            Assert.Equal(writeBytes * (k + 1), workers[0].GetUsedBytes());
        }

        for (int k = 0; k < 5; k++)
        {
            TachyonURI fileURI = new TachyonURI(uniqPath + k);
            int fileId = sTfs.GetFileId(fileURI);
            sTfs.Delete(fileId, true);
            Assert.False(sTfs.Exist(fileURI));

            CommonUtils.SleepMs(null, SLEEP_MS);
            workers = sTfs.GetWorkersInfo();
            Assert.Equal(writeBytes * (4 - k), workers[0].GetUsedBytes());
        }
    }

    [Fact]
    public void DeleteFileTest3()
    {
        string uniqPath = TestUtils.UniqPath();
        List<ClientWorkerInfo> workers = sTfs.GetWorkersInfo();
        Assert.Equal(0, workers[0].GetUsedBytes());
        int writeBytes = USER_QUOTA_UNIT_BYTES * 2;

        // Delete non-existing files.
        Assert.True(sTfs.Delete(new TachyonURI(uniqPath), false));
        Assert.True(sTfs.Delete(new TachyonURI(uniqPath), true));

        for (int k = 0; k < 5; k++)
        {
            TachyonURI fileURI = new TachyonURI(uniqPath + k);
            int fileId = TestUtils.CreateByteFile(sTfs, fileURI, WriteType.MUST_CACHE, writeBytes);
            TachyonFile file = sTfs.GetFile(fileId);
            Assert.True(file.IsInMemory());
            Assert.True(sTfs.Exist(fileURI));

            workers = sTfs.GetWorkersInfo();
            Assert.Equal(writeBytes * (k + 1), workers[0].GetUsedBytes());
        }

        for (int k = 0; k < 5; k++)
        {
            TachyonURI fileURI = new TachyonURI(uniqPath + k);
            int fileId = sTfs.GetFileId(fileURI);
            sTfs.Delete(fileId, true);
            Assert.False(sTfs.Exist(fileURI));

            CommonUtils.SleepMs(null, SLEEP_MS);
            workers = sTfs.GetWorkersInfo();
            Assert.Equal(1, workers.Count);
            Assert.Equal(WORKER_CAPACITY_BYTES, workers[0].GetCapacityBytes());
            Assert.Equal(writeBytes * (4 - k), workers[0].GetUsedBytes());
        }
    }
}
