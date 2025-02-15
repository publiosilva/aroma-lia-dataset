import pytest

class TestHRegion:

    def test_delete_mixed(self):
        table_name = b'testtable'
        fam = b'info'
        families = [fam]
        method = self.test_delete_mixed.__name__
        self.init_hregion(table_name, method, families)

        row = b'table_name'
        serverinfo = b'serverinfo'
        splitA = b'splitA'
        splitB = b'splitB'

        put = Put(row)
        put.add(fam, splitA, b'reference_A')
        region.put(put)

        put = Put(row)
        put.add(fam, splitB, b'reference_B')
        region.put(put)

        put = Put(row)
        put.add(fam, serverinfo, b'ip_address')
        region.put(put)

        delete = Delete(row)
        delete.delete_columns(fam, splitA)
        region.delete(delete, None, True)

        get = Get(row).add_column(fam, serverinfo)
        result = region.get(get, None)
        assert len(result) == 1

        get = Get(row).add_column(fam, splitA)
        result = region.get(get, None)
        assert len(result) == 0

        get = Get(row).add_column(fam, splitB)
        result = region.get(get, None)
        assert len(result) == 1

        put = Put(row)
        put.add(fam, splitA, b'reference_A')
        region.put(put)
        get = Get(row)
        result = region.get(get, None)
        assert len(result) == 3

        delete = Delete(row)
        region.delete(delete, None, False)
        assert len(region.get(get, None)) == 0
        
        time.sleep(0.01)
        region.put(Put(row).add(fam, splitA, b'reference_A'))
        result = region.get(get, None)
        assert len(result) == 1
