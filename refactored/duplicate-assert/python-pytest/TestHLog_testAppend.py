import pytest

class TestHLog:

    def test_append1(self):
        COL_COUNT = 10
        table_name = Bytes.toBytes("tablename")
        row = Bytes.toBytes("row")
        self.conf.setBoolean("dfs.support.append", True)
        reader = None
        log = HLog(self.fs, dir, self.oldLogDir, self.conf, None)
        try:
            timestamp = time.time()
            cols = WALEdit()
            for i in range(COL_COUNT):
                cols.add(KeyValue(row, Bytes.toBytes("column"),
                                  Bytes.toBytes(str(i)),
                                  timestamp, bytes([i + ord('0')])))
            hri = HRegionInfo(HTableDescriptor(table_name),
                              HConstants.EMPTY_START_ROW, HConstants.EMPTY_END_ROW)
            log.append(hri, table_name, cols, time.time())
            log_seq_id = log.startCacheFlush()
            log.completeCacheFlush(hri.getRegionName(), table_name, log_seq_id, False)
            log.close()
            filename = log.computeFilename(log.getFilenum())
            log = None
            reader = HLog.getReader(self.fs, filename, self.conf)
            entry = reader.next()
            assert COL_COUNT == entry.getEdit().size()
            idx = 0
            for val in entry.getEdit().getKeyValues():
                assert Bytes.equals(hri.getRegionName(), entry.getKey().getRegionName())
                assert Bytes.equals(table_name, entry.getKey().getTablename())
                assert Bytes.equals(row, val.getRow())
                assert (byte(idx + ord('0'))) == val.getValue()[0]
                print(entry.getKey(), val)
                idx += 1

            entry = reader.next()
            assert 1 == entry.getEdit().size()
            for val in entry.getEdit().getKeyValues():
                assert Bytes.equals(HLog.METAROW, val.getRow())
                assert Bytes.equals(HLog.METAFAMILY, val.getFamily())
                assert 0 == Bytes.compareTo(HLog.COMPLETE_CACHE_FLUSH, val.getValue())
                print(entry.getKey(), val)
        finally:
            if log is not None:
                log.closeAndDelete()
            if reader is not None:
                reader.close()

    def test_append2(self):
        COL_COUNT = 10
        table_name = Bytes.toBytes("tablename")
        row = Bytes.toBytes("row")
        self.conf.setBoolean("dfs.support.append", True)
        reader = None
        log = HLog(self.fs, dir, self.oldLogDir, self.conf, None)
        try:
            timestamp = time.time()
            cols = WALEdit()
            for i in range(COL_COUNT):
                cols.add(KeyValue(row, Bytes.toBytes("column"),
                                  Bytes.toBytes(str(i)),
                                  timestamp, bytes([i + ord('0')])))
            hri = HRegionInfo(HTableDescriptor(table_name),
                              HConstants.EMPTY_START_ROW, HConstants.EMPTY_END_ROW)
            log.append(hri, table_name, cols, time.time())
            log_seq_id = log.startCacheFlush()
            log.completeCacheFlush(hri.getRegionName(), table_name, log_seq_id, False)
            log.close()
            filename = log.computeFilename(log.getFilenum())
            log = None
            reader = HLog.getReader(self.fs, filename, self.conf)
            entry = reader.next()
            assert COL_COUNT == entry.getEdit().size()
            idx = 0
            for val in entry.getEdit().getKeyValues():
                assert Bytes.equals(row, val.getRow())
                assert (byte(idx + ord('0'))) == val.getValue()[0]
                print(entry.getKey(), val)
                idx += 1

            entry = reader.next()
            assert 1 == entry.getEdit().size()
            for val in entry.getEdit().getKeyValues():
                assert Bytes.equals(hri.getRegionName(), entry.getKey().getRegionName())
                assert Bytes.equals(table_name, entry.getKey().getTablename())
                assert Bytes.equals(HLog.METAROW, val.getRow())
                assert Bytes.equals(HLog.METAFAMILY, val.getFamily())
                assert 0 == Bytes.compareTo(HLog.COMPLETE_CACHE_FLUSH, val.getValue())
                print(entry.getKey(), val)
        finally:
            if log is not None:
                log.closeAndDelete()
            if reader is not None:
                reader.close()
