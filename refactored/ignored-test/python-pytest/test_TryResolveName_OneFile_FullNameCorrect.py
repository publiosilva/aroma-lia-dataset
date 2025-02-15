import pytest
import os

# @pytest.mark.disabled
class TestLiveUnitTestingDirectoryResolver:

    @pytest.mark.parametrize("test_name, expected_folder_path, expected_filename", 
                             [("Test1.Foo", "Test1.cs", "Test1.Foo")])
    def test_try_resolve_name_one_file_full_name_correct(self, test_name, expected_folder_path, expected_filename):
        # arrange
        temp_dir = ArrangeLiveUnitTestDirectory("Test1.cs")
        # act
        full_name = LiveUnitTestingDirectoryResolver.try_resolve_name(test_name)
        # assert
        assert full_name is not None
        assert os.path.join(temp_dir, "1") == full_name.get_folder_path()
        assert expected_filename == full_name.get_filename()
