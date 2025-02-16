public class TestResourceTrackerService {
  @Test
  public void testDecommissionWithExcludeHosts1() throws Exception {
    Configuration conf = new Configuration();
    conf.set(YarnConfiguration.RM_NODES_EXCLUDE_FILE_PATH, hostFile
        .getAbsolutePath());

    writeToHostsFile("");
    rm = new MockRM(conf);
    rm.start();

    MockNM nm1 = rm.registerNode("host1:1234", 5120);
    MockNM nm2 = rm.registerNode("host2:5678", 10240);
    MockNM nm3 = rm.registerNode("localhost:4433", 1024);

    int metricCount = ClusterMetrics.getMetrics().getNumDecommisionedNMs();

    NodeHeartbeatResponse nodeHeartbeat = nm1.nodeHeartbeat(true);
    Assert.assertTrue(NodeAction.NORMAL.equals(nodeHeartbeat.getNodeAction()));
  }

  @Test
  public void testDecommissionWithExcludeHosts2() throws Exception {
    Configuration conf = new Configuration();
    conf.set(YarnConfiguration.RM_NODES_EXCLUDE_FILE_PATH, hostFile
        .getAbsolutePath());

    writeToHostsFile("");
    rm = new MockRM(conf);
    rm.start();

    MockNM nm1 = rm.registerNode("host1:1234", 5120);
    MockNM nm2 = rm.registerNode("host2:5678", 10240);
    MockNM nm3 = rm.registerNode("localhost:4433", 1024);

    int metricCount = ClusterMetrics.getMetrics().getNumDecommisionedNMs();

    NodeHeartbeatResponse nodeHeartbeat = nm1.nodeHeartbeat(true);
    nodeHeartbeat = nm2.nodeHeartbeat(true);
    Assert.assertTrue(NodeAction.NORMAL.equals(nodeHeartbeat.getNodeAction()));
  }

  @Test
  public void testDecommissionWithExcludeHosts3() throws Exception {
    Configuration conf = new Configuration();
    conf.set(YarnConfiguration.RM_NODES_EXCLUDE_FILE_PATH, hostFile
        .getAbsolutePath());

    writeToHostsFile("");
    rm = new MockRM(conf);
    rm.start();

    MockNM nm1 = rm.registerNode("host1:1234", 5120);
    MockNM nm2 = rm.registerNode("host2:5678", 10240);
    MockNM nm3 = rm.registerNode("localhost:4433", 1024);

    int metricCount = ClusterMetrics.getMetrics().getNumDecommisionedNMs();

    NodeHeartbeatResponse nodeHeartbeat = nm1.nodeHeartbeat(true);
    nodeHeartbeat = nm2.nodeHeartbeat(true);

    // To test that IPs also work
    String ip = NetUtils.normalizeHostName("localhost");
    writeToHostsFile("host2", ip);

    rm.getNodesListManager().refreshNodes(conf);

    nodeHeartbeat = nm1.nodeHeartbeat(true);
    Assert.assertTrue(NodeAction.NORMAL.equals(nodeHeartbeat.getNodeAction()));
    nodeHeartbeat = nm2.nodeHeartbeat(true);
    Assert.assertTrue("The decommisioned metrics are not updated",
        NodeAction.SHUTDOWN.equals(nodeHeartbeat.getNodeAction()));
  }

  @Test
  public void testDecommissionWithExcludeHosts4() throws Exception {
    Configuration conf = new Configuration();
    conf.set(YarnConfiguration.RM_NODES_EXCLUDE_FILE_PATH, hostFile
        .getAbsolutePath());

    writeToHostsFile("");
    rm = new MockRM(conf);
    rm.start();

    MockNM nm1 = rm.registerNode("host1:1234", 5120);
    MockNM nm2 = rm.registerNode("host2:5678", 10240);
    MockNM nm3 = rm.registerNode("localhost:4433", 1024);

    int metricCount = ClusterMetrics.getMetrics().getNumDecommisionedNMs();

    NodeHeartbeatResponse nodeHeartbeat = nm1.nodeHeartbeat(true);
    nodeHeartbeat = nm2.nodeHeartbeat(true);

    // To test that IPs also work
    String ip = NetUtils.normalizeHostName("localhost");
    writeToHostsFile("host2", ip);

    rm.getNodesListManager().refreshNodes(conf);

    nodeHeartbeat = nm1.nodeHeartbeat(true);
    Assert.assertTrue(NodeAction.NORMAL.equals(nodeHeartbeat.getNodeAction()));
    nodeHeartbeat = nm2.nodeHeartbeat(true);
    checkDecommissionedNMCount(rm, ++metricCount);

    nodeHeartbeat = nm3.nodeHeartbeat(true);
    Assert.assertTrue("The decommisioned metrics are not updated",
        NodeAction.SHUTDOWN.equals(nodeHeartbeat.getNodeAction()));
    checkDecommissionedNMCount(rm, ++metricCount);
  }
}
