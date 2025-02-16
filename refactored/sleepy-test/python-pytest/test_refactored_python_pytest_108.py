import pytest

class TestJMSAccessorService:
    def test_connection_retry_max_attempt(self):
        services.destroy()
        services = super().setup_services_for_hcatalog()
        jndi_properties_string = f"java.naming.factory.initial#{ActiveMQConnFactory};" \
                                  f"java.naming.provider.url#tcp://localhost:12345;connectionFactoryNames#ConnectionFactory"
        services_conf = services.get_conf()
        services_conf.set(JMSAccessorService.CONF_RETRY_INITIAL_DELAY, "1")
        services_conf.set(JMSAccessorService.CONF_RETRY_MAX_ATTEMPTS, "1")
        services_conf.set(HCatAccessorService.JMS_CONNECTIONS_PROPERTIES, f"default={jndi_properties_string}")
        services.init()
        hcat_service = Services.get().get(HCatAccessorService)
        jms_service = Services.get().get(JMSAccessorService)

        publisher_authority = "hcat.server.com:5080"
        topic = "topic.topic1"
        conn_info = hcat_service.get_jms_connection_info(URI("hcat://hcat.server.com:8020"))
        jms_service.register_for_notification(conn_info, topic, HCatMessageHandler(publisher_authority))
        assert jms_service.is_connection_in_retry_list(conn_info)
        assert jms_service.is_topic_in_retry_list(conn_info, topic)
        assert not jms_service.is_listening_to_topic(conn_info, topic)
        # time.sleep(1.1)
        assert jms_service.is_connection_in_retry_list(conn_info)
        assert jms_service.is_topic_in_retry_list(conn_info, topic)
        assert not jms_service.is_listening_to_topic(conn_info, topic)
        assert jms_service.get_num_connection_attempts(conn_info) == 1
        assert not jms_service.retry_connection(conn_info)
        jms_service.destroy()
