import pytest

class TestHLog:

    def test_append(self):
        COL_COUNT = 10
        table_name = b'tablename'
        row = b'row'
        self.conf.setboolean("dfs.support.append", True)
        reader = None
        log = HLog(self.fs, dir, self.old_log_dir, self.conf, None)
        try:
            timestamp = int(time.time() * 1000)
            cols = WALEdit()
            for i in range(COL_COUNT):
                cols.add(KeyValue(row, b'column', str(i).encode(), timestamp, bytes([i + ord('0')])))
            hri = HRegionInfo(HTableDescriptor(table_name), HConstants.EMPTY_START_ROW, HConstants.EMPTY_END_ROW)
            log.append(hri, table_name, cols, int(time.time() * 1000))
            log_seq_id = log.start_cache_flush()
            log.complete_cache_flush(hri.get_region_name(), table_name, log_seq_id, False)
            log.close()
            filename = log.compute_filename(log.get_filenum())
            log = None
            reader = HLog.get_reader(self.fs, filename, self.conf)
            entry = reader.next()
            assert len(entry.get_edit()) == COL_COUNT
            idx = 0
            for val in entry.get_edit().get_key_values():
                assert Bytes.equals(hri.get_region_name(), entry.get_key().get_region_name())
                assert Bytes.equals(table_name, entry.get_key().get_tablename())
                assert Bytes.equals(row, val.get_row())
                assert val.get_value()[0] == bytes([idx + ord('0')])
                print(entry.get_key(), val)
                idx += 1

            entry = reader.next()
            assert len(entry.get_edit()) == 1
            for val in entry.get_edit().get_key_values():
                assert Bytes.equals(hri.get_region_name(), entry.get_key().get_region_name())
                assert Bytes.equals(table_name, entry.get_key().get_tablename())
                assert Bytes.equals(HLog.METAROW, val.get_row())
                assert Bytes.equals(HLog.METAFAMILY, val.get_family())
                assert Bytes.compare_to(HLog.COMPLETE_CACHE_FLUSH, val.get_value()) == 0
                print(entry.get_key(), val)
        finally:
            if log is not None:
                log.close_and_delete()
            if reader is not None:
                reader.close()
