public class TestRowResource : HBaseRESTClusterTestBase
{
    [Fact]
    public void TestSingleCellGetPutPB()
    {
        Response response = GetValuePB(TABLE, ROW_1, COLUMN_1);
        Assert.Equal(404, response.Code);

        response = PutValuePB(TABLE, ROW_1, COLUMN_1, VALUE_1);
        Assert.Equal(200, response.Code);
        CheckValuePB(TABLE, ROW_1, COLUMN_1, VALUE_1);

        response = PutValuePB(TABLE, ROW_1, COLUMN_1, VALUE_1);
        Assert.Equal(200, response.Code);
        CheckValuePB(TABLE, ROW_1, COLUMN_1, VALUE_1);
        response = PutValueXML(TABLE, ROW_1, COLUMN_1, VALUE_2);
        Assert.Equal(200, response.Code);
        CheckValuePB(TABLE, ROW_1, COLUMN_1, VALUE_2);

        response = DeleteRow(TABLE, ROW_1);
        Assert.Equal(200, response.Code);
    }
}
