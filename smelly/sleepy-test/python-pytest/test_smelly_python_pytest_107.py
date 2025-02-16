import pytest

class TestJMSAccessorService:
    def test_connection_retry(self):
        services.destroy()
        services = super().setup_services_for_hcatalog()
        random_port = 30000 + random.randint(0, 9999)
        broker_url = f"tcp://localhost:{random_port}"
        services_conf = services.get_conf()
        services_conf.set(JMSAccessorService.CONF_RETRY_INITIAL_DELAY, "1")
        services_conf.set(JMSAccessorService.CONF_RETRY_MAX_ATTEMPTS, "3")
        services_conf.set(HCatAccessorService.JMS_CONNECTIONS_PROPERTIES, f"default=java.naming.factory.initial#"
                f"{ActiveMQConnFactory};java.naming.provider.url#{broker_url};connectionFactoryNames#"
                f"ConnectionFactory")
        services.init()
        hcat_service = Services.get().get(HCatAccessorService)
        jms_service = Services.get().get(JMSAccessorService)

        publisher_authority = "hcat.server.com:5080"
        topic = "topic.topic1"
        conn_info = hcat_service.get_jms_connection_info(URI("hcat://hcat.server.com:8020"))
        jms_service.register_for_notification(conn_info, topic, HCatMessageHandler(publisher_authority))
        assert not jms_service.is_listening_to_topic(conn_info, topic)
        assert jms_service.is_connection_in_retry_list(conn_info)
        assert jms_service.is_topic_in_retry_list(conn_info, topic)
        
        broker = BrokerService()
        broker.add_connector(broker_url)
        broker.start()
        time.sleep(1)
        assert jms_service.is_listening_to_topic(conn_info, topic)
        assert not jms_service.is_connection_in_retry_list(conn_info)
        assert not jms_service.is_topic_in_retry_list(conn_info, topic)
        broker.stop()
        jms_service.destroy()
