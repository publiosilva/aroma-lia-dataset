import pytest

@pytest.mark.timeout(4000)
def test_00():
    hook_hot_deploy_listener = HookHotDeployListener()
    assert hook_hot_deploy_listener is not None
    
    mock_servlet_context = MockServletContext()
    assert mock_servlet_context is not None
    assert mock_servlet_context.getServerInfo() is None
    assert mock_servlet_context.getMajorVersion() == 0
    assert mock_servlet_context.getMinorVersion() == 0
    assert mock_servlet_context.getServletContextName() is None
    
    hot_deploy_event = HotDeployEvent(mock_servlet_context, None)
    assert hot_deploy_event is not None
    assert mock_servlet_context.getServerInfo() is None
    assert mock_servlet_context.getMajorVersion() == 0
    assert mock_servlet_context.getMinorVersion() == 0
    assert mock_servlet_context.getServletContextName() is None
    
    hook_hot_deploy_listener.doInvokeUndeploy(hot_deploy_event)
    assert mock_servlet_context.getServerInfo() is None
    assert mock_servlet_context.getMajorVersion() == 0
    assert mock_servlet_context.getMinorVersion() == 0
    assert mock_servlet_context.getServletContextName() is None
