public class TestRowResource extends TestCase {
  public void testSingleCellGetPutPB1() throws IOException, JAXBException {
    Response response = getValuePB(TABLE, ROW_1, COLUMN_1);
    assertEquals(response.getCode(), 404);

    response = putValuePB(TABLE, ROW_1, COLUMN_1, VALUE_1);
    assertEquals(response.getCode(), 200);
  }

  public void testSingleCellGetPutPB2() throws IOException, JAXBException {
    Response response = getValuePB(TABLE, ROW_1, COLUMN_1);
    assertEquals(response.getCode(), 404);

    response = putValuePB(TABLE, ROW_1, COLUMN_1, VALUE_1);
    checkValuePB(TABLE, ROW_1, COLUMN_1, VALUE_1);

    response = putValuePB(TABLE, ROW_1, COLUMN_1, VALUE_1);
    assertEquals(response.getCode(), 200);    
  }

  public void testSingleCellGetPutPB3() throws IOException, JAXBException {
    Response response = getValuePB(TABLE, ROW_1, COLUMN_1);

    response = putValuePB(TABLE, ROW_1, COLUMN_1, VALUE_1);
    checkValuePB(TABLE, ROW_1, COLUMN_1, VALUE_1);

    response = putValuePB(TABLE, ROW_1, COLUMN_1, VALUE_1);
    checkValuePB(TABLE, ROW_1, COLUMN_1, VALUE_1);
    response = putValueXML(TABLE, ROW_1, COLUMN_1, VALUE_2);
    assertEquals(response.getCode(), 200);   
  }

  public void testSingleCellGetPutPB4() throws IOException, JAXBException {
    Response response = getValuePB(TABLE, ROW_1, COLUMN_1);
    assertEquals(response.getCode(), 404);

    response = putValuePB(TABLE, ROW_1, COLUMN_1, VALUE_1);
    checkValuePB(TABLE, ROW_1, COLUMN_1, VALUE_1);

    response = putValuePB(TABLE, ROW_1, COLUMN_1, VALUE_1);
    checkValuePB(TABLE, ROW_1, COLUMN_1, VALUE_1);
    response = putValueXML(TABLE, ROW_1, COLUMN_1, VALUE_2);
    checkValuePB(TABLE, ROW_1, COLUMN_1, VALUE_2);

    response = deleteRow(TABLE, ROW_1);
    assertEquals(response.getCode(), 200);    
  }
}
