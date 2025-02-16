import pytest

@pytest.mark.timeout(4000)
def test_00():
    hook_hot_deploy_listener = HookHotDeployListener()
    assert hook_hot_deploy_listener is not None

    mock_servlet_context = MockServletContext()
    assert mock_servlet_context is not None
    assert mock_servlet_context.get_server_info() is None
    assert mock_servlet_context.get_major_version() == 0
    assert mock_servlet_context.get_minor_version() == 0
    assert mock_servlet_context.get_servlet_context_name() is None

    hot_deploy_event = HotDeployEvent(mock_servlet_context, None)
    assert hot_deploy_event is not None
    assert mock_servlet_context.get_server_info() is None
    assert mock_servlet_context.get_major_version() == 0
    assert mock_servlet_context.get_minor_version() == 0
    assert mock_servlet_context.get_servlet_context_name() is None

    hook_hot_deploy_listener.do_invoke_undeploy(hot_deploy_event)
    assert mock_servlet_context.get_server_info() is None
    assert mock_servlet_context.get_major_version() == 0
    assert mock_servlet_context.get_minor_version() == 0
    assert mock_servlet_context.get_servlet_context_name() is None
