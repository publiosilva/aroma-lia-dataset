import pytest
import os

class TestLiveUnitTestingDirectoryResolver:
    
    @pytest.mark.skip(reason="")
    def test_try_resolve_name_one_file_full_name_correct(self):
        # arrange
        temp_dir = arrange_live_unit_test_directory("Test1.cs")
        test_name = "Test1.Foo"
        # act
        full_name = LiveUnitTestingDirectoryResolver.try_resolve_name(test_name)
        # assert
        assert full_name is not None
        assert full_name.folder_path == os.path.join(temp_dir, "1")
        assert full_name.filename == test_name
