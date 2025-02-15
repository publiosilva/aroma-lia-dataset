import pytest

class TestResourceTrackerService:
    def test_decommission_with_exclude_hosts(self):
        conf = Configuration()
        conf.set(YarnConfiguration.RM_NODES_EXCLUDE_FILE_PATH, hostFile.get_absolute_path())

        write_to_hosts_file("")
        rm = MockRM(conf)
        rm.start()

        nm1 = rm.register_node("host1:1234", 5120)
        nm2 = rm.register_node("host2:5678", 10240)
        nm3 = rm.register_node("localhost:4433", 1024)

        metric_count = ClusterMetrics.get_metrics().get_num_decommissioned_nms()

        node_heartbeat = nm1.node_heartbeat(True)
        assert NodeAction.NORMAL == node_heartbeat.get_node_action()
        node_heartbeat = nm2.node_heartbeat(True)
        assert NodeAction.NORMAL == node_heartbeat.get_node_action()

        ip = NetUtils.normalize_host_name("localhost")
        write_to_hosts_file("host2", ip)

        rm.get_nodes_list_manager().refresh_nodes(conf)

        node_heartbeat = nm1.node_heartbeat(True)
        assert NodeAction.NORMAL == node_heartbeat.get_node_action()
        node_heartbeat = nm2.node_heartbeat(True)
        assert NodeAction.SHUTDOWN == node_heartbeat.get_node_action(), "The decommissioned metrics are not updated"
        check_decommissioned_nm_count(rm, metric_count + 1)

        node_heartbeat = nm3.node_heartbeat(True)
        assert NodeAction.SHUTDOWN == node_heartbeat.get_node_action(), "The decommissioned metrics are not updated"
        check_decommissioned_nm_count(rm, metric_count + 1)
