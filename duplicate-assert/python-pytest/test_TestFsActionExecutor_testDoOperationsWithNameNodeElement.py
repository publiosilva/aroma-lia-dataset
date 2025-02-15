import pytest

class TestFsActionExecutor:
    def test_do_operations_with_name_node_element(self):
        ae = FsActionExecutor()
        fs = get_file_system()

        mkdir = Path(get_fs_test_case_dir(), "mkdir")
        mkdir_x = Path(mkdir.to_uri().path)
        delete = Path(get_fs_test_case_dir(), "delete")
        delete_x = Path(delete.to_uri().path)
        fs.mkdirs(delete)
        source = Path(get_fs_test_case_dir(), "source")
        source_x = Path(source.to_uri().path)
        fs.mkdirs(source)
        target = Path(Path(get_fs_test_case_dir(), "target").to_uri().path)
        chmod1 = Path(get_fs_test_case_dir(), "chmod1")
        chmod1_x = Path(chmod1.to_uri().path)
        fs.mkdirs(chmod1)
        child1 = Path(chmod1, "child1")
        fs.mkdirs(child1)
        chmod2 = Path(get_fs_test_case_dir(), "chmod2")
        chmod2_x = Path(chmod2.to_uri().path)
        fs.mkdirs(chmod2)
        child2 = Path(chmod2, "child2")
        fs.mkdirs(child2)
        new_file1 = Path(mkdir + "newFile1")
        new_file1_x = Path(new_file1.to_uri().path)
        new_file2 = Path(mkdir + "newFile2")
        new_file2_x = Path(new_file2.to_uri().path)
        fs.create_new_file(new_file1)
        chmod3 = Path(get_fs_test_case_dir(), "chmod3")
        chmod3_x = Path(chmod3.to_uri().path)
        fs.mkdirs(chmod3)
        child3 = Path(chmod3, "child3")
        fs.mkdirs(child3)
        grandchild3 = Path(child3, "grandchild1")
        fs.mkdirs(grandchild3)

        str_ = "<root><name-node>{0}</name-node>" + \
                "<mkdir path=''{1}''/>" + \
                "<delete path=''{2}''/>" + \
                "<move source=''{3}'' target=''{4}''/>" + \
                "<chmod path=''{5}'' permissions=''-rwxrwxrwx''/>" + \
                "<chmod path=''{6}'' permissions=''-rwxrwx---'' dir-files=''false''/>" + \
                "<touchz path=''{7}''/>" + \
                "<touchz path=''{8}''/>" + \
                "<chmod path=''{9}'' permissions=''-rwxrwx---''> <recursive/> </chmod>" + \
                "</root>".format(get_name_node_uri(), mkdir_x, delete_x, source_x, target, chmod1_x, chmod2_x, new_file1_x, new_file2_x, chmod3_x)

        xml = XmlUtils.parse_xml(str_)

        ae.do_operations(create_context("<fs/>"), xml)

        assert fs.exists(mkdir)
        assert not fs.exists(delete)
        assert not fs.exists(source)
        assert fs.exists(target)
        assert fs.exists(new_file1)
        assert fs.exists(new_file2)

        assert fs.get_file_status(chmod1).get_permission().to_string() == "rwxrwxrwx"
        assert fs.get_file_status(child1).get_permission().to_string() != "rwxrwxrwx"
        assert fs.get_file_status(chmod2).get_permission().to_string() == "rwxrwx---"
        assert fs.get_file_status(child2).get_permission().to_string() != "rwxrwx---"

        assert fs.get_file_status(child3).get_permission().to_string() == "rwxrwx---"
        assert fs.get_file_status(grandchild3).get_permission().to_string() == "rwxrwx---"

        assert fs.get_file_status(child3).get_permission().to_string() == "rwxrwx---"
        assert fs.get_file_status(grandchild3).get_permission().to_string() == "rwxrwx---"
