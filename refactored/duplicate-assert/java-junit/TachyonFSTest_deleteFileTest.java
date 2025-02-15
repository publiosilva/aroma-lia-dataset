public class TachyonFSTest {
  @Test
  public void deleteFileTest1() throws IOException {
    String uniqPath = TestUtils.uniqPath();
    List<ClientWorkerInfo> workers = sTfs.getWorkersInfo();
    Assert.assertEquals(1, workers.size());
    Assert.assertEquals(WORKER_CAPACITY_BYTES, workers.get(0).getCapacityBytes());
    Assert.assertEquals(0, workers.get(0).getUsedBytes());
    int writeBytes = USER_QUOTA_UNIT_BYTES * 2;

    // Delete non-existing files.
    Assert.assertTrue(sTfs.delete(new TachyonURI(uniqPath), false));
    Assert.assertTrue(sTfs.delete(new TachyonURI(uniqPath), true));

    for (int k = 0; k < 5; k ++) {
      TachyonURI fileURI = new TachyonURI(uniqPath + k);
      int fileId = TestUtils.createByteFile(sTfs, fileURI, WriteType.MUST_CACHE, writeBytes);
      TachyonFile file = sTfs.getFile(fileId);
      Assert.assertTrue(file.isInMemory());
      Assert.assertTrue(sTfs.exist(fileURI));

      workers = sTfs.getWorkersInfo();
      Assert.assertEquals(writeBytes * (k + 1), workers.get(0).getUsedBytes());
    }

    for (int k = 0; k < 5; k ++) {
      TachyonURI fileURI = new TachyonURI(uniqPath + k);
      int fileId = sTfs.getFileId(fileURI);
      sTfs.delete(fileId, true);
      Assert.assertFalse(sTfs.exist(fileURI));

      CommonUtils.sleepMs(null, SLEEP_MS);
      workers = sTfs.getWorkersInfo();
      Assert.assertEquals(writeBytes * (4 - k), workers.get(0).getUsedBytes());
    }
  }

  @Test
  public void deleteFileTest2() throws IOException {
    String uniqPath = TestUtils.uniqPath();
    List<ClientWorkerInfo> workers = sTfs.getWorkersInfo();
    Assert.assertEquals(0, workers.get(0).getUsedBytes());
    int writeBytes = USER_QUOTA_UNIT_BYTES * 2;

    // Delete non-existing files.
    Assert.assertTrue(sTfs.delete(new TachyonURI(uniqPath), false));
    Assert.assertTrue(sTfs.delete(new TachyonURI(uniqPath), true));

    for (int k = 0; k < 5; k ++) {
      TachyonURI fileURI = new TachyonURI(uniqPath + k);
      int fileId = TestUtils.createByteFile(sTfs, fileURI, WriteType.MUST_CACHE, writeBytes);
      TachyonFile file = sTfs.getFile(fileId);
      Assert.assertTrue(file.isInMemory());
      Assert.assertTrue(sTfs.exist(fileURI));

      workers = sTfs.getWorkersInfo();
      Assert.assertEquals(1, workers.size());
      Assert.assertEquals(WORKER_CAPACITY_BYTES, workers.get(0).getCapacityBytes());
      Assert.assertEquals(writeBytes * (k + 1), workers.get(0).getUsedBytes());
    }

    for (int k = 0; k < 5; k ++) {
      TachyonURI fileURI = new TachyonURI(uniqPath + k);
      int fileId = sTfs.getFileId(fileURI);
      sTfs.delete(fileId, true);
      Assert.assertFalse(sTfs.exist(fileURI));

      CommonUtils.sleepMs(null, SLEEP_MS);
      workers = sTfs.getWorkersInfo();
      Assert.assertEquals(writeBytes * (4 - k), workers.get(0).getUsedBytes());
    }
  }

  @Test
  public void deleteFileTest3() throws IOException {
    String uniqPath = TestUtils.uniqPath();
    List<ClientWorkerInfo> workers = sTfs.getWorkersInfo();
    Assert.assertEquals(0, workers.get(0).getUsedBytes());
    int writeBytes = USER_QUOTA_UNIT_BYTES * 2;

    // Delete non-existing files.
    Assert.assertTrue(sTfs.delete(new TachyonURI(uniqPath), false));
    Assert.assertTrue(sTfs.delete(new TachyonURI(uniqPath), true));

    for (int k = 0; k < 5; k ++) {
      TachyonURI fileURI = new TachyonURI(uniqPath + k);
      int fileId = TestUtils.createByteFile(sTfs, fileURI, WriteType.MUST_CACHE, writeBytes);
      TachyonFile file = sTfs.getFile(fileId);
      Assert.assertTrue(file.isInMemory());
      Assert.assertTrue(sTfs.exist(fileURI));

      workers = sTfs.getWorkersInfo();
      Assert.assertEquals(writeBytes * (k + 1), workers.get(0).getUsedBytes());
    }

    for (int k = 0; k < 5; k ++) {
      TachyonURI fileURI = new TachyonURI(uniqPath + k);
      int fileId = sTfs.getFileId(fileURI);
      sTfs.delete(fileId, true);
      Assert.assertFalse(sTfs.exist(fileURI));

      CommonUtils.sleepMs(null, SLEEP_MS);
      workers = sTfs.getWorkersInfo();
      Assert.assertEquals(1, workers.size());
      Assert.assertEquals(WORKER_CAPACITY_BYTES, workers.get(0).getCapacityBytes());
      Assert.assertEquals(writeBytes * (4 - k), workers.get(0).getUsedBytes());
    }
  }
}
