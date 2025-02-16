public class TestRowResource {
    [Fact]
    public void TestSingleCellGetPutPB1() {
        Response response = GetValuePB(TABLE, ROW_1, COLUMN_1);
        Assert.Equal(404, response.GetCode());

        response = PutValuePB(TABLE, ROW_1, COLUMN_1, VALUE_1);
        Assert.Equal(200, response.GetCode());
    }

    [Fact]
    public void TestSingleCellGetPutPB2() {
        Response response = GetValuePB(TABLE, ROW_1, COLUMN_1);
        Assert.Equal(404, response.GetCode());

        response = PutValuePB(TABLE, ROW_1, COLUMN_1, VALUE_1);
        CheckValuePB(TABLE, ROW_1, COLUMN_1, VALUE_1);

        response = PutValuePB(TABLE, ROW_1, COLUMN_1, VALUE_1);
        Assert.Equal(200, response.GetCode());
    }

    [Fact]
    public void TestSingleCellGetPutPB3() {
        Response response = GetValuePB(TABLE, ROW_1, COLUMN_1);

        response = PutValuePB(TABLE, ROW_1, COLUMN_1, VALUE_1);
        CheckValuePB(TABLE, ROW_1, COLUMN_1, VALUE_1);

        response = PutValuePB(TABLE, ROW_1, COLUMN_1, VALUE_1);
        CheckValuePB(TABLE, ROW_1, COLUMN_1, VALUE_1);
        response = PutValueXML(TABLE, ROW_1, COLUMN_1, VALUE_2);
        Assert.Equal(200, response.GetCode());
    }

    [Fact]
    public void TestSingleCellGetPutPB4() {
        Response response = GetValuePB(TABLE, ROW_1, COLUMN_1);
        Assert.Equal(404, response.GetCode());

        response = PutValuePB(TABLE, ROW_1, COLUMN_1, VALUE_1);
        CheckValuePB(TABLE, ROW_1, COLUMN_1, VALUE_1);

        response = PutValuePB(TABLE, ROW_1, COLUMN_1, VALUE_1);
        CheckValuePB(TABLE, ROW_1, COLUMN_1, VALUE_1);
        response = PutValueXML(TABLE, ROW_1, COLUMN_1, VALUE_2);
        CheckValuePB(TABLE, ROW_1, COLUMN_1, VALUE_2);

        response = DeleteRow(TABLE, ROW_1);
        Assert.Equal(200, response.GetCode());
    }
}
