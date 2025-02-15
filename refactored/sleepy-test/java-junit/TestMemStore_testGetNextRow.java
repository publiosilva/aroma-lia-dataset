public class TestMemStore extends TestCase {
  public void testGetNextRow() throws Exception {
    addRows(this.memstore);
    // Add more versions to make it a little more interesting.
    // Thread.sleep(1);
    addRows(this.memstore);
    KeyValue closestToEmpty = this.memstore.getNextRow(KeyValue.LOWESTKEY);
    assertTrue(KeyValue.COMPARATOR.compareRows(closestToEmpty,
      new KeyValue(Bytes.toBytes(0), System.currentTimeMillis())) == 0);
    for (int i = 0; i < ROW_COUNT; i++) {
      KeyValue nr = this.memstore.getNextRow(new KeyValue(Bytes.toBytes(i),
        System.currentTimeMillis()));
      if (i + 1 == ROW_COUNT) {
        assertEquals(nr, null);
      } else {
        assertTrue(KeyValue.COMPARATOR.compareRows(nr,
          new KeyValue(Bytes.toBytes(i + 1), System.currentTimeMillis())) == 0);
      }
    }
    //starting from each row, validate results should contain the starting row
    for (int startRowId = 0; startRowId < ROW_COUNT; startRowId++) {
      InternalScanner scanner =
          new StoreScanner(new Scan(Bytes.toBytes(startRowId)), FAMILY,
              Integer.MAX_VALUE, this.memstore.comparator, null,
              memstore.getScanners());
      List<KeyValue> results = new ArrayList<KeyValue>();
      for (int i = 0; scanner.next(results); i++) {
        int rowId = startRowId + i;
        assertTrue("Row name",
          KeyValue.COMPARATOR.compareRows(results.get(0),
          Bytes.toBytes(rowId)) == 0);
        assertEquals("Count of columns", QUALIFIER_COUNT, results.size());
        List<KeyValue> row = new ArrayList<KeyValue>();
        for (KeyValue kv : results) {
          row.add(kv);
        }
        isExpectedRowWithoutTimestamps(rowId, row);
        // Clear out set.  Otherwise row results accumulate.
        results.clear();
      }
    }
  }
}
