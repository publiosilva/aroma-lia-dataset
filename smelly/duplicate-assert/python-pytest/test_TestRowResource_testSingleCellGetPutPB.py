import pytest

class TestRowResource:

    def test_single_cell_get_put_pb(self):
        response = get_value_pb(TABLE, ROW_1, COLUMN_1)
        assert response.get_code() == 404

        response = put_value_pb(TABLE, ROW_1, COLUMN_1, VALUE_1)
        assert response.get_code() == 200
        check_value_pb(TABLE, ROW_1, COLUMN_1, VALUE_1)

        response = put_value_pb(TABLE, ROW_1, COLUMN_1, VALUE_1)
        assert response.get_code() == 200
        check_value_pb(TABLE, ROW_1, COLUMN_1, VALUE_1)
        response = put_value_xml(TABLE, ROW_1, COLUMN_1, VALUE_2)
        assert response.get_code() == 200
        check_value_pb(TABLE, ROW_1, COLUMN_1, VALUE_2)

        response = delete_row(TABLE, ROW_1)
        assert response.get_code() == 200
