import pytest

class TestMemStore:
    def test_get_next_row(self):
        self.add_rows(self.memstore)
        time.sleep(1)
        self.add_rows(self.memstore)
        closest_to_empty = self.memstore.get_next_row(KeyValue.LOWESTKEY)
        assert KeyValue.COMPARATOR.compare_rows(closest_to_empty, KeyValue(Bytes.to_bytes(0), time.time_ns())) == 0
        for i in range(ROW_COUNT):
            nr = self.memstore.get_next_row(KeyValue(Bytes.to_bytes(i), time.time_ns()))
            if i + 1 == ROW_COUNT:
                assert nr is None
            else:
                assert KeyValue.COMPARATOR.compare_rows(nr, KeyValue(Bytes.to_bytes(i + 1), time.time_ns())) == 0
        for start_row_id in range(ROW_COUNT):
            scanner = StoreScanner(Scan(Bytes.to_bytes(start_row_id)), FAMILY, 
                                   float('inf'), self.memstore.comparator, None,
                                   self.memstore.get_scanners())
            results = []
            for i in range(scanner.next(results)):
                row_id = start_row_id + i
                assert KeyValue.COMPARATOR.compare_rows(results[0], Bytes.to_bytes(row_id)) == 0
                assert len(results) == QUALIFIER_COUNT
                row = [kv for kv in results]
                self.is_expected_row_without_timestamps(row_id, row)
                results.clear()
