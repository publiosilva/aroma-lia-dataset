public class TestHRegion : HBaseTestCase
{
    [Fact]
    public void TestDeleteMixed()
    {
        byte[] tableName = Encoding.UTF8.GetBytes("testtable");
        byte[] fam = Encoding.UTF8.GetBytes("info");
        byte[][] families = { fam };
        string method = this.GetType().GetMethod(nameof(TestDeleteMixed)).Name;
        InitHRegion(tableName, method, families);

        byte[] row = Encoding.UTF8.GetBytes("table_name");
        byte[] serverinfo = Encoding.UTF8.GetBytes("serverinfo");
        byte[] splitA = Encoding.UTF8.GetBytes("splitA");
        byte[] splitB = Encoding.UTF8.GetBytes("splitB");

        Put put = new Put(row);
        put.Add(fam, splitA, Encoding.UTF8.GetBytes("reference_A"));
        region.Put(put);

        put = new Put(row);
        put.Add(fam, splitB, Encoding.UTF8.GetBytes("reference_B"));
        region.Put(put);

        put = new Put(row);
        put.Add(fam, serverinfo, Encoding.UTF8.GetBytes("ip_address"));
        region.Put(put);

        Delete delete = new Delete(row);
        delete.DeleteColumns(fam, splitA);
        region.Delete(delete, null, true);

        Get get = new Get(row).AddColumn(fam, serverinfo);
        Result result = region.Get(get, null);
        Assert.Equal(1, result.Size());

        get = new Get(row).AddColumn(fam, splitA);
        result = region.Get(get, null);
        Assert.Equal(0, result.Size());

        get = new Get(row).AddColumn(fam, splitB);
        result = region.Get(get, null);
        Assert.Equal(1, result.Size());

        put = new Put(row);
        put.Add(fam, splitA, Encoding.UTF8.GetBytes("reference_A"));
        region.Put(put);
        get = new Get(row);
        result = region.Get(get, null);
        Assert.Equal(3, result.Size());

        delete = new Delete(row);
        region.Delete(delete, null, false);
        Assert.Equal(0, region.Get(get, null).Size());
        // Thread.Sleep(10);
        region.Put(new Put(row).Add(fam, splitA, Encoding.UTF8.GetBytes("reference_A")));
        result = region.Get(get, null);
        Assert.Equal(1, result.Size());
    }
}
