public class TestHeartbeatMonitor 
{
    [Fact]
    public void TestHeartbeatLossWithComponent() 
    {
        var clusters = injector.GetInstance<Clusters>();
        clusters.AddHost(hostname1);
        clusters.GetHost(hostname1).SetOsType("centos5");
        clusters.GetHost(hostname1).Persist();
        clusters.AddCluster(clusterName);
        var cluster = clusters.GetCluster(clusterName);
        cluster.SetDesiredStackVersion(new StackId("HDP-0.1"));

        var hostNames = new HashSet<string> { hostname1 };

        clusters.MapHostsToCluster(hostNames, clusterName);

        var hdfs = cluster.AddService(serviceName);
        hdfs.Persist();
        hdfs.AddServiceComponent(Role.DATANODE.ToString()).Persist();
        hdfs.GetServiceComponent(Role.DATANODE.ToString()).AddServiceComponentHost(hostname1).Persist();
        hdfs.AddServiceComponent(Role.NAMENODE.ToString()).Persist();
        hdfs.GetServiceComponent(Role.NAMENODE.ToString()).AddServiceComponentHost(hostname1).Persist();
        hdfs.AddServiceComponent(Role.SECONDARY_NAMENODE.ToString()).Persist();
        hdfs.GetServiceComponent(Role.SECONDARY_NAMENODE.ToString()).AddServiceComponentHost(hostname1).Persist();
        hdfs.AddServiceComponent(Role.HDFS_CLIENT.ToString()).Persist();
        hdfs.GetServiceComponent(Role.HDFS_CLIENT.ToString()).AddServiceComponentHost(hostname1);

        var aq = new ActionQueue();
        var am = new Mock<ActionManager>();
        var hm = new HeartbeatMonitor(clusters, aq, am.Object, 10);
        var handler = new HeartBeatHandler(clusters, aq, am.Object, injector);

        var reg = new Register
        {
            Hostname = hostname1,
            ResponseId = 12,
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - 300,
            AgentVersion = ambariMetaInfo.GetServerVersion()
        };

        var hi = new HostInfo { OS = "Centos5" };
        reg.HardwareProfile = hi;
        handler.HandleRegistration(reg);

        var hb = new HeartBeat
        {
            Hostname = hostname1,
            NodeStatus = new HostStatus(HostStatus.Status.HEALTHY, "cool"),
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
            ResponseId = 12
        };
        handler.HandleHeartBeat(hb);
        hm.Start();
        aq.Enqueue(hostname1, new ExecutionCommand());
        
        while (aq.Size(hostname1) != 0) 
        {
            Thread.Sleep(1);
        }

        cluster = clusters.GetClustersForHost(hostname1).First();
        foreach (var sch in cluster.GetServiceComponentHosts(hostname1)) 
        {
            var s = cluster.GetService(sch.GetServiceName());
            var sc = s.GetServiceComponent(sch.GetServiceComponentName());
            if (!sc.IsClientComponent())
                Assert.Equal(State.UNKNOWN, sch.GetState());
            else
                Assert.Equal(State.INIT, sch.GetState());
        }

        hm.Shutdown();
        
        hb = new HeartBeat
        {
            Hostname = hostname1,
            NodeStatus = new HostStatus(HostStatus.Status.HEALTHY, "cool"),
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
            ResponseId = 0
        };
        
        var statuses = new List<ComponentStatus>
        {
            new ComponentStatus 
            {
                ClusterName = clusterName,
                ServiceName = Service.Type.HDFS.ToString(),
                Status = State.STARTED.ToString(),
                ComponentName = Role.DATANODE.ToString()
            },
            new ComponentStatus 
            {
                ClusterName = clusterName,
                ServiceName = Service.Type.HDFS.ToString(),
                Status = State.STARTED.ToString(),
                ComponentName = Role.NAMENODE.ToString()
            },
            new ComponentStatus 
            {
                ClusterName = clusterName,
                ServiceName = Service.Type.HDFS.ToString(),
                Status = State.STARTED.ToString(),
                ComponentName = Role.SECONDARY_NAMENODE.ToString()
            }
        };

        hb.ComponentStatus = statuses;

        var host = clusters.GetHost(hostname1);
        host.SetState(HostState.HEALTHY);
        hb.Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + int.MaxValue;
        host.SetLastHeartbeatTime(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + int.MaxValue);
        handler.HandleHeartBeat(hb);

        cluster = clusters.GetClustersForHost(hostname1).First();
        foreach (var sch in cluster.GetServiceComponentHosts(hostname1)) 
        {
            var s = cluster.GetService(sch.GetServiceName());
            var sc = s.GetServiceComponent(sch.GetServiceComponentName());
            if (!sc.IsClientComponent())
                Assert.Equal(State.STARTED, sch.GetState());
            else
                Assert.Equal(State.INIT, sch.GetState());
        }
    }
}
