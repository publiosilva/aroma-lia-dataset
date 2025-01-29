def test_clear_child_count_cache():
    parent = DetailAstImpl()
    child = DetailAstImpl()
    parent.setFirstChild(child)

    clear_child_count_cache_methods = [
        child.setNextSibling,
        child.addPreviousSibling,
        child.addNextSibling
    ]

    for method in clear_child_count_cache_methods:
        start_count = parent.getChildCount()
        method(None)
        intermediate_count = Whitebox.getInternalState(parent, "childCount")
        finish_count = parent.getChildCount()
        assert start_count == finish_count, "Child count has changed"
        assert intermediate_count == float("-inf"), "Invalid child count"

    start_count = child.getChildCount()
    child.addChild(None)
    intermediate_count = Whitebox.getInternalState(child, "childCount")
    finish_count = child.getChildCount()
    assert start_count == finish_count, "Child count has changed"
    assert intermediate_count == float("-inf"), "Invalid child count"
