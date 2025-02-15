public class TestMemStore 
{
    [Fact]
    public void TestGetNextRow()
    {
        AddRows(this.memstore);
        Thread.Sleep(1);
        AddRows(this.memstore);
        KeyValue closestToEmpty = this.memstore.GetNextRow(KeyValue.LOWESTKEY);
        Assert.True(KeyValue.COMPARATOR.CompareRows(closestToEmpty, new KeyValue(Bytes.ToBytes(0), DateTime.Now.Ticks)) == 0);
        for (int i = 0; i < ROW_COUNT; i++)
        {
            KeyValue nr = this.memstore.GetNextRow(new KeyValue(Bytes.ToBytes(i), DateTime.Now.Ticks));
            if (i + 1 == ROW_COUNT)
            {
                Assert.Null(nr);
            }
            else
            {
                Assert.True(KeyValue.COMPARATOR.CompareRows(nr, new KeyValue(Bytes.ToBytes(i + 1), DateTime.Now.Ticks)) == 0);
            }
        }
        for (int startRowId = 0; startRowId < ROW_COUNT; startRowId++)
        {
            InternalScanner scanner = new StoreScanner(new Scan(Bytes.ToBytes(startRowId)), FAMILY, int.MaxValue, this.memstore.Comparator, null, memstore.GetScanners());
            List<KeyValue> results = new List<KeyValue>();
            for (int i = 0; scanner.Next(results); i++)
            {
                int rowId = startRowId + i;
                Assert.True(KeyValue.COMPARATOR.CompareRows(results[0], Bytes.ToBytes(rowId)) == 0, "Row name");
                Assert.Equal(QUALIFIER_COUNT, results.Count);
                List<KeyValue> row = new List<KeyValue>();
                foreach (KeyValue kv in results)
                {
                    row.Add(kv);
                }
                IsExpectedRowWithoutTimestamps(rowId, row);
                results.Clear();
            }
        }
    }
}
