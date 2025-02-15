public class TestResourceTrackerService
{
    [Fact]
    public void TestDecommissionWithExcludeHosts1()
    {
        var conf = new Configuration();
        conf.Set(YarnConfiguration.RM_NODES_EXCLUDE_FILE_PATH, hostFile.AbsolutePath);

        WriteToHostsFile("");
        rm = new MockRM(conf);
        rm.Start();

        var nm1 = rm.RegisterNode("host1:1234", 5120);
        var nm2 = rm.RegisterNode("host2:5678", 10240);
        var nm3 = rm.RegisterNode("localhost:4433", 1024);

        var metricCount = ClusterMetrics.GetMetrics().GetNumDecommissionedNMs();

        var nodeHeartbeat = nm1.NodeHeartbeat(true);
        Assert.True(NodeAction.NORMAL.Equals(nodeHeartbeat.GetNodeAction()));
    }

    [Fact]
    public void TestDecommissionWithExcludeHosts2()
    {
        var conf = new Configuration();
        conf.Set(YarnConfiguration.RM_NODES_EXCLUDE_FILE_PATH, hostFile.AbsolutePath);

        WriteToHostsFile("");
        rm = new MockRM(conf);
        rm.Start();

        var nm1 = rm.RegisterNode("host1:1234", 5120);
        var nm2 = rm.RegisterNode("host2:5678", 10240);
        var nm3 = rm.RegisterNode("localhost:4433", 1024);

        var metricCount = ClusterMetrics.GetMetrics().GetNumDecommissionedNMs();

        var nodeHeartbeat = nm1.NodeHeartbeat(true);
        nodeHeartbeat = nm2.NodeHeartbeat(true);
        Assert.True(NodeAction.NORMAL.Equals(nodeHeartbeat.GetNodeAction()));
    }

    [Fact]
    public void TestDecommissionWithExcludeHosts3()
    {
        var conf = new Configuration();
        conf.Set(YarnConfiguration.RM_NODES_EXCLUDE_FILE_PATH, hostFile.AbsolutePath);

        WriteToHostsFile("");
        rm = new MockRM(conf);
        rm.Start();

        var nm1 = rm.RegisterNode("host1:1234", 5120);
        var nm2 = rm.RegisterNode("host2:5678", 10240);
        var nm3 = rm.RegisterNode("localhost:4433", 1024);

        var metricCount = ClusterMetrics.GetMetrics().GetNumDecommissionedNMs();

        var nodeHeartbeat = nm1.NodeHeartbeat(true);
        nodeHeartbeat = nm2.NodeHeartbeat(true);

        var ip = NetUtils.NormalizeHostName("localhost");
        WriteToHostsFile("host2", ip);

        rm.GetNodesListManager().RefreshNodes(conf);

        nodeHeartbeat = nm1.NodeHeartbeat(true);
        Assert.True(NodeAction.NORMAL.Equals(nodeHeartbeat.GetNodeAction()));
        nodeHeartbeat = nm2.NodeHeartbeat(true);
        Assert.True("The decommissioned metrics are not updated", NodeAction.SHUTDOWN.Equals(nodeHeartbeat.GetNodeAction()));
    }

    [Fact]
    public void TestDecommissionWithExcludeHosts4()
    {
        var conf = new Configuration();
        conf.Set(YarnConfiguration.RM_NODES_EXCLUDE_FILE_PATH, hostFile.AbsolutePath);

        WriteToHostsFile("");
        rm = new MockRM(conf);
        rm.Start();

        var nm1 = rm.RegisterNode("host1:1234", 5120);
        var nm2 = rm.RegisterNode("host2:5678", 10240);
        var nm3 = rm.RegisterNode("localhost:4433", 1024);

        var metricCount = ClusterMetrics.GetMetrics().GetNumDecommissionedNMs();

        var nodeHeartbeat = nm1.NodeHeartbeat(true);
        nodeHeartbeat = nm2.NodeHeartbeat(true);

        var ip = NetUtils.NormalizeHostName("localhost");
        WriteToHostsFile("host2", ip);

        rm.GetNodesListManager().RefreshNodes(conf);

        nodeHeartbeat = nm1.NodeHeartbeat(true);
        Assert.True(NodeAction.NORMAL.Equals(nodeHeartbeat.GetNodeAction()));
        nodeHeartbeat = nm2.NodeHeartbeat(true);
        CheckDecommissionedNMCount(rm, ++metricCount);

        nodeHeartbeat = nm3.NodeHeartbeat(true);
        Assert.True("The decommissioned metrics are not updated", NodeAction.SHUTDOWN.Equals(nodeHeartbeat.GetNodeAction()));
        CheckDecommissionedNMCount(rm, ++metricCount);
    }
}
