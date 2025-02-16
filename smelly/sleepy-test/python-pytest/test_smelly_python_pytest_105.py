import pytest
from unittest.mock import Mock

def test_heartbeat_loss_with_component():
    clusters = injector.get_instance(Clusters)
    clusters.add_host(hostname1)
    clusters.get_host(hostname1).set_os_type("centos5")
    clusters.get_host(hostname1).persist()
    clusters.add_cluster(cluster_name)
    cluster = clusters.get_cluster(cluster_name)
    cluster.set_desired_stack_version(StackId("HDP-0.1"))

    host_names = {hostname1}
    clusters.map_hosts_to_cluster(host_names, cluster_name)

    hdfs = cluster.add_service(service_name)
    hdfs.persist()
    hdfs.add_service_component(Role.DATANODE.name()).persist()
    hdfs.get_service_component(Role.DATANODE.name()).add_service_component_host(hostname1).persist()
    hdfs.add_service_component(Role.NAMENODE.name()).persist()
    hdfs.get_service_component(Role.NAMENODE.name()).add_service_component_host(hostname1).persist()
    hdfs.add_service_component(Role.SECONDARY_NAMENODE.name()).persist()
    hdfs.get_service_component(Role.SECONDARY_NAMENODE.name()).add_service_component_host(hostname1).persist()
    hdfs.add_service_component(Role.HDFS_CLIENT.name()).persist()
    hdfs.get_service_component(Role.HDFS_CLIENT.name()).add_service_component_host(hostname1)
    
    aq = ActionQueue()
    am = Mock(ActionManager)
    hm = HeartbeatMonitor(clusters, aq, am, 10)
    handler = HeartBeatHandler(clusters, aq, am, injector)
    
    reg = Register()
    reg.set_hostname(hostname1)
    reg.set_response_id(12)
    reg.set_timestamp(System.current_time_millis() - 300)
    reg.set_agent_version(ambari_meta_info.get_server_version())
    
    hi = HostInfo()
    hi.set_os("Centos5")
    reg.set_hardware_profile(hi)
    handler.handle_registration(reg)
    
    hb = HeartBeat()
    hb.set_hostname(hostname1)
    hb.set_node_status(HostStatus(HostStatus.Status.HEALTHY, "cool"))
    hb.set_timestamp(System.current_time_millis())
    hb.set_response_id(12)
    handler.handle_heartbeat(hb)
    hm.start()
    aq.enqueue(hostname1, ExecutionCommand())
    
    while aq.size(hostname1) != 0:
        sleep(1)

    cluster = next(iter(clusters.get_clusters_for_host(hostname1)))
    for sch in cluster.get_service_component_hosts(hostname1):
        s = cluster.get_service(sch.get_service_name())
        sc = s.get_service_component(sch.get_service_component_name())
        if not sc.is_client_component():
            assert sch.get_state() == State.UNKNOWN
        else:
            assert sch.get_state() == State.INIT

    hm.shutdown()
    
    hb = HeartBeat()
    hb.set_hostname(hostname1)
    hb.set_node_status(HostStatus(HostStatus.Status.HEALTHY, "cool"))
    hb.set_timestamp(System.current_time_millis())
    hb.set_response_id(0)
    
    statuses = []
    for role_name in [Role.DATANODE, Role.NAMENODE, Role.SECONDARY_NAMENODE]:
        cs = ComponentStatus()
        cs.set_cluster_name(cluster_name)
        cs.set_service_name(Service.Type.HDFS.name())
        cs.set_status(State.STARTED.name())
        cs.set_component_name(role_name.name())
        statuses.append(cs)

    hb.set_component_status(statuses)
    
    host = clusters.get_host(hostname1)
    host.set_state(HostState.HEALTHY)
    hb.set_timestamp(System.current_time_millis() + float('inf'))
    host.set_last_heartbeat_time(System.current_time_millis() + float('inf'))
    handler.handle_heartbeat(hb)
    
    cluster = next(iter(clusters.get_clusters_for_host(hostname1)))
    for sch in cluster.get_service_component_hosts(hostname1):
        s = cluster.get_service(sch.get_service_name())
        sc = s.get_service_component(sch.get_service_component_name())
        if not sc.is_client_component():
            assert sch.get_state() == State.STARTED
        else:
            assert sch.get_state() == State.INIT
