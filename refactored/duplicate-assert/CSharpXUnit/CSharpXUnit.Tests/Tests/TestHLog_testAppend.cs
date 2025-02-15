using System;
using System.IO;
using Xunit;

public class TestHLog {
  
  [Fact]
  public void TestAppend1() {
    const int COL_COUNT = 10;
    byte[] tableName = Bytes.ToBytes("tablename");
    byte[] row = Bytes.ToBytes("row");
    this.conf.SetBoolean("dfs.support.append", true);
    Reader reader = null;
    HLog log = new HLog(this.fs, dir, this.oldLogDir, this.conf, null);
    try {
      long timestamp = DateTime.UtcNow.Ticks;
      WALEdit cols = new WALEdit();
      for (int i = 0; i < COL_COUNT; i++) {
        cols.Add(new KeyValue(row, Bytes.ToBytes("column"),
          Bytes.ToBytes(i.ToString()),
          timestamp, new byte[] { (byte)(i + '0') }));
      }
      HRegionInfo hri = new HRegionInfo(new HTableDescriptor(tableName),
          HConstants.EMPTY_START_ROW, HConstants.EMPTY_END_ROW);
      log.Append(hri, tableName, cols, DateTime.UtcNow.Ticks);
      long logSeqId = log.StartCacheFlush();
      log.CompleteCacheFlush(hri.GetRegionName(), tableName, logSeqId, false);
      log.Close();
      Path filename = log.ComputeFilename(log.GetFilenum());
      log = null;
      reader = HLog.GetReader(fs, filename, conf);
      HLog.Entry entry = reader.Next();
      Assert.Equal(COL_COUNT, entry.GetEdit().Size());
      int idx = 0;
      foreach (KeyValue val in entry.GetEdit().GetKeyValues()) {
        Assert.True(Bytes.Equals(hri.GetRegionName(),
          entry.GetKey().GetRegionName()));
        Assert.True(Bytes.Equals(tableName, entry.GetKey().GetTablename()));
        Assert.True(Bytes.Equals(row, val.GetRow()));
        Assert.Equal((byte)(idx + '0'), val.GetValue()[0]);
        Console.WriteLine(entry.GetKey() + " " + val);
        idx++;
      }

      entry = reader.Next();
      Assert.Equal(1, entry.GetEdit().Size());
      foreach (KeyValue val in entry.GetEdit().GetKeyValues()) {
        Assert.True(Bytes.Equals(HLog.METAROW, val.GetRow()));
        Assert.True(Bytes.Equals(HLog.METAFAMILY, val.GetFamily()));
        Assert.Equal(0, Bytes.CompareTo(HLog.COMPLETE_CACHE_FLUSH,
          val.GetValue()));
        Console.WriteLine(entry.GetKey() + " " + val);
      }
    } finally {
      if (log != null) {
        log.CloseAndDelete();
      }
      if (reader != null) {
        reader.Close();
      }
    }
  }

  [Fact]
  public void TestAppend2() {
    const int COL_COUNT = 10;
    byte[] tableName = Bytes.ToBytes("tablename");
    byte[] row = Bytes.ToBytes("row");
    this.conf.SetBoolean("dfs.support.append", true);
    Reader reader = null;
    HLog log = new HLog(this.fs, dir, this.oldLogDir, this.conf, null);
    try {
      long timestamp = DateTime.UtcNow.Ticks;
      WALEdit cols = new WALEdit();
      for (int i = 0; i < COL_COUNT; i++) {
        cols.Add(new KeyValue(row, Bytes.ToBytes("column"),
          Bytes.ToBytes(i.ToString()),
          timestamp, new byte[] { (byte)(i + '0') }));
      }
      HRegionInfo hri = new HRegionInfo(new HTableDescriptor(tableName),
          HConstants.EMPTY_START_ROW, HConstants.EMPTY_END_ROW);
      log.Append(hri, tableName, cols, DateTime.UtcNow.Ticks);
      long logSeqId = log.StartCacheFlush();
      log.CompleteCacheFlush(hri.GetRegionName(), tableName, logSeqId, false);
      log.Close();
      Path filename = log.ComputeFilename(log.GetFilenum());
      log = null;
      reader = HLog.GetReader(fs, filename, conf);
      HLog.Entry entry = reader.Next();
      Assert.Equal(COL_COUNT, entry.GetEdit().Size());
      int idx = 0;
      foreach (KeyValue val in entry.GetEdit().GetKeyValues()) {
        Assert.True(Bytes.Equals(row, val.GetRow()));
        Assert.Equal((byte)(idx + '0'), val.GetValue()[0]);
        Console.WriteLine(entry.GetKey() + " " + val);
        idx++;
      }

      entry = reader.Next();
      Assert.Equal(1, entry.GetEdit().Size());
      foreach (KeyValue val in entry.GetEdit().GetKeyValues()) {
        Assert.True(Bytes.Equals(hri.GetRegionName(),
          entry.GetKey().GetRegionName()));
        Assert.True(Bytes.Equals(tableName, entry.GetKey().GetTablename()));
        Assert.True(Bytes.Equals(HLog.METAROW, val.GetRow()));
        Assert.True(Bytes.Equals(HLog.METAFAMILY, val.GetFamily()));
        Assert.Equal(0, Bytes.CompareTo(HLog.COMPLETE_CACHE_FLUSH,
          val.GetValue()));
        Console.WriteLine(entry.GetKey() + " " + val);
      }
    } finally {
      if (log != null) {
        log.CloseAndDelete();
      }
      if (reader != null) {
        reader.Close();
      }
    }
  }
}
