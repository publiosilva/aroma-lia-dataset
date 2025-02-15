import pytest

class TestSpecificationProvider:
    
    def test_should_collect_data_sets_from_tags(self):
        sut = create_sut()
        result = sut.get_specification([Tag(None, r'@DataSource:path\to\file.csv'), Tag(None, r'@DataSet:data-set-name')], SOURCE_FILE_PATH)
        assert result is not None
        assert result.data_set is not None
        assert result.data_set == "data-set-name"
