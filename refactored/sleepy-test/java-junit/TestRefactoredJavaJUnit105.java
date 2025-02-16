public class TestHeartbeatMonitor {
  @Test
  public void testHeartbeatLossWithComponent() throws AmbariException, InterruptedException,
          InvalidStateTransitionException {
    
    Clusters clusters = injector.getInstance(Clusters.class);
    clusters.addHost(hostname1);
    clusters.getHost(hostname1).setOsType("centos5");
    clusters.getHost(hostname1).persist();
    clusters.addCluster(clusterName);
    Cluster cluster = clusters.getCluster(clusterName);
    cluster.setDesiredStackVersion(new StackId("HDP-0.1"));

    Set<String> hostNames = new HashSet<String>(){{
      add(hostname1);
     }};

    clusters.mapHostsToCluster(hostNames, clusterName);

    Service hdfs = cluster.addService(serviceName);
    hdfs.persist();
    hdfs.addServiceComponent(Role.DATANODE.name()).persist();
    hdfs.getServiceComponent(Role.DATANODE.name()).addServiceComponentHost(hostname1).persist();
    hdfs.addServiceComponent(Role.NAMENODE.name()).persist();
    hdfs.getServiceComponent(Role.NAMENODE.name()).addServiceComponentHost(hostname1).persist();
    hdfs.addServiceComponent(Role.SECONDARY_NAMENODE.name()).persist();
    hdfs.getServiceComponent(Role.SECONDARY_NAMENODE.name()).addServiceComponentHost(hostname1).persist();
    hdfs.addServiceComponent(Role.HDFS_CLIENT.name()).persist();
    hdfs.getServiceComponent(Role.HDFS_CLIENT.name()).addServiceComponentHost(hostname1);
    
    ActionQueue aq = new ActionQueue();
    ActionManager am = mock(ActionManager.class);
    HeartbeatMonitor hm = new HeartbeatMonitor(clusters, aq, am, 10);
    HeartBeatHandler handler = new HeartBeatHandler(clusters, aq, am, injector);
    
    Register reg = new Register();
    reg.setHostname(hostname1);
    reg.setResponseId(12);
    reg.setTimestamp(System.currentTimeMillis() - 300);
    reg.setAgentVersion(ambariMetaInfo.getServerVersion());
    
    HostInfo hi = new HostInfo();
    hi.setOS("Centos5");
    reg.setHardwareProfile(hi);
    handler.handleRegistration(reg);
    
    HeartBeat hb = new HeartBeat();
    hb.setHostname(hostname1);
    hb.setNodeStatus(new HostStatus(HostStatus.Status.HEALTHY, "cool"));
    hb.setTimestamp(System.currentTimeMillis());
    hb.setResponseId(12);
    handler.handleHeartBeat(hb);
    hm.start();
    aq.enqueue(hostname1, new ExecutionCommand());
    //Heartbeat will expire and action queue will be flushed
    while (aq.size(hostname1) != 0) {
      // Thread.sleep(1);
    }

    cluster = clusters.getClustersForHost(hostname1).iterator().next();
    for (ServiceComponentHost sch : cluster.getServiceComponentHosts(hostname1)) {
      Service s = cluster.getService(sch.getServiceName());
      ServiceComponent sc = s.getServiceComponent(sch.getServiceComponentName());
      if (!sc.isClientComponent())
        assertEquals(State.UNKNOWN, sch.getState());
      else
        assertEquals(State.INIT, sch.getState());
    }

    // don't keep marking the host as down
    hm.shutdown();
    
    // try to flip statuses back
    hb = new HeartBeat();
    hb.setHostname(hostname1);
    hb.setNodeStatus(new HostStatus(HostStatus.Status.HEALTHY, "cool"));
    hb.setTimestamp(System.currentTimeMillis());
    hb.setResponseId(0);
    
    List<ComponentStatus> statuses = new ArrayList<ComponentStatus>();
    ComponentStatus cs = new ComponentStatus();
    cs.setClusterName(clusterName);
    cs.setServiceName(Service.Type.HDFS.name());
    cs.setStatus(State.STARTED.name());
    cs.setComponentName(Role.DATANODE.name());
    statuses.add(cs);

    cs = new ComponentStatus();
    cs.setClusterName(clusterName);
    cs.setServiceName(Service.Type.HDFS.name());
    cs.setStatus(State.STARTED.name());
    cs.setComponentName(Role.NAMENODE.name());
    statuses.add(cs);
    
    cs = new ComponentStatus();
    cs.setClusterName(clusterName);
    cs.setServiceName(Service.Type.HDFS.name());
    cs.setStatus(State.STARTED.name());
    cs.setComponentName(Role.SECONDARY_NAMENODE.name());
    statuses.add(cs);
    
    hb.setComponentStatus(statuses);
    
    Host host = clusters.getHost(hostname1);
    host.setState(HostState.HEALTHY);
    hb.setTimestamp(System.currentTimeMillis() + Integer.MAX_VALUE);
    host.setLastHeartbeatTime(System.currentTimeMillis() + Integer.MAX_VALUE);
    handler.handleHeartBeat(hb);
    
    cluster = clusters.getClustersForHost(hostname1).iterator().next();
    for (ServiceComponentHost sch : cluster.getServiceComponentHosts(hostname1)) {
      Service s = cluster.getService(sch.getServiceName());
      ServiceComponent sc = s.getServiceComponent(sch.getServiceComponentName());
      if (!sc.isClientComponent())
        assertEquals(State.STARTED, sch.getState());
      else
        assertEquals(State.INIT, sch.getState());
    }
    
    
  }
}
