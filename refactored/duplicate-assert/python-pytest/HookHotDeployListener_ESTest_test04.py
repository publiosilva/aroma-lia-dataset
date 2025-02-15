import pytest

class TestHookHotDeployListener:
    
    @pytest.mark.timeout(4000)
    def test001(self):
        hook_hot_deploy_listener_0 = HookHotDeployListener()
        assert hook_hot_deploy_listener_0 is not None
        
        mock_servlet_context_0 = MockServletContext()
        assert mock_servlet_context_0 is not None
        assert mock_servlet_context_0.getServerInfo() is None
        assert mock_servlet_context_0.getMajorVersion() == 0
        assert mock_servlet_context_0.getMinorVersion() == 0
        assert mock_servlet_context_0.getServletContextName() is None

    @pytest.mark.timeout(4000)
    def test002(self):
        hook_hot_deploy_listener_0 = HookHotDeployListener()
        mock_servlet_context_0 = MockServletContext()
        hot_deploy_event_0 = HotDeployEvent(mock_servlet_context_0, None)
        assert hot_deploy_event_0 is not None
        assert mock_servlet_context_0.getServerInfo() is None
        assert mock_servlet_context_0.getMajorVersion() == 0
        assert mock_servlet_context_0.getMinorVersion() == 0
        assert mock_servlet_context_0.getServletContextName() is None

    @pytest.mark.timeout(4000)
    def test003(self):
        hook_hot_deploy_listener_0 = HookHotDeployListener()
        mock_servlet_context_0 = MockServletContext()
        hot_deploy_event_0 = HotDeployEvent(mock_servlet_context_0, None)
        hook_hot_deploy_listener_0.doInvokeUndeploy(hot_deploy_event_0)
        assert mock_servlet_context_0.getServerInfo() is None
        assert mock_servlet_context_0.getMajorVersion() == 0
        assert mock_servlet_context_0.getMinorVersion() == 0
        assert mock_servlet_context_0.getServletContextName() is None
