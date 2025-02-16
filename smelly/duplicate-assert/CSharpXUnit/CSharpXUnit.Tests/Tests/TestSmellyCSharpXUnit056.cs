public class TestResourceTrackerService
{
    [Fact]
    public void TestDecommissionWithExcludeHosts()
    {
        var conf = new Configuration();
        conf.Set(YarnConfiguration.RM_NODES_EXCLUDE_FILE_PATH, hostFile
            .FullName);

        WriteToHostsFile("");
        rm = new MockRM(conf);
        rm.Start();

        var nm1 = rm.RegisterNode("host1:1234", 5120);
        var nm2 = rm.RegisterNode("host2:5678", 10240);
        var nm3 = rm.RegisterNode("localhost:4433", 1024);

        int metricCount = ClusterMetrics.GetMetrics().GetNumDecommisionedNMs();

        NodeHeartbeatResponse nodeHeartbeat = nm1.NodeHeartbeat(true);
        Assert.True(NodeAction.NORMAL.Equals(nodeHeartbeat.NodeAction));
        nodeHeartbeat = nm2.NodeHeartbeat(true);
        Assert.True(NodeAction.NORMAL.Equals(nodeHeartbeat.NodeAction));

        // To test that IPs also work
        string ip = NetUtils.NormalizeHostName("localhost");
        WriteToHostsFile("host2", ip);

        rm.GetNodesListManager().RefreshNodes(conf);

        nodeHeartbeat = nm1.NodeHeartbeat(true);
        Assert.True(NodeAction.NORMAL.Equals(nodeHeartbeat.NodeAction));
        nodeHeartbeat = nm2.NodeHeartbeat(true);
        Assert.True(NodeAction.SHUTDOWN.Equals(nodeHeartbeat.NodeAction), "The decommisioned metrics are not updated");
        CheckDecommissionedNMCount(rm, ++metricCount);

        nodeHeartbeat = nm3.NodeHeartbeat(true);
        Assert.True(NodeAction.SHUTDOWN.Equals(nodeHeartbeat.NodeAction), "The decommisioned metrics are not updated");
        CheckDecommissionedNMCount(rm, ++metricCount);
    }
}
