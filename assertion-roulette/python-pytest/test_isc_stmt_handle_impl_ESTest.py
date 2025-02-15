@pytest.mark.timeout(4000)
def test_00():
    isc_stmt_handle_impl0 = isc_stmt_handle_impl()
    isc_stmt_handle_impl0.ensure_capacity(1010)
    isc_stmt_handle_impl0.ensure_capacity(0)
    assert not isc_stmt_handle_impl0.is_all_rows_fetched()
    assert isc_stmt_handle_impl0.size() == 0
    assert not isc_stmt_handle_impl0.is_singleton_result()
