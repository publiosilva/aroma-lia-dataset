def test_extensions():
    check = FileLengthCheck()
    check.setFileExtensions("java")
    assert check.getFileExtensions()[0] == ".java", "extension should be the same"
    check.setFileExtensions(".java")
    assert check.getFileExtensions()[0] == ".java", "extension should be the same"
    with pytest.raises(IllegalArgumentException) as ex:
        check.setFileExtensions(None)
    assert str(ex.value) == "Extensions array can not be null", "Invalid exception message"
