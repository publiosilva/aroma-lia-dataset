import pytest

class TestHCatAccessorService:
    def test_get_jms_connection_info(self):
        services.destroy()
        services = super().setup_services_for_hcatalog()
        conf = services.get_conf()
        server1 = "default=java.naming.factory.initial#org.apache.activemq.jndi.ActiveMQInitialContextFactory;" + \
                   "java.naming.provider.url#vm://localhost?broker.persistent=false"
        server2 = "hcat://${1}.${2}.server.com:8020=java.naming.factory.initial#Dummy.Factory;" + \
                   "java.naming.provider.url#tcp://broker.${2}:61616"
        server3 = "hcat://xyz.corp.dummy.com=java.naming.factory.initial#Dummy.Factory;" + \
                   "java.naming.provider.url#tcp:localhost:61616"

        jms_connection_url = server1 + "," + server2 + "," + server3
        conf.set(HCatAccessorService.JMS_CONNECTIONS_PROPERTIES, jms_connection_url)
        services.init()

        hcat_service = services.get(HCatAccessorService)
        conn_info = hcat_service.get_jms_connection_info(URI("hcat://hcatserver.blue.server.com:8020"))
        # rules will be applied
        assert conn_info.get_jndi_properties_string() == "java.naming.factory.initial#Dummy.Factory;java.naming.provider.url#tcp://broker.blue:61616"

        conn_info = hcat_service.get_jms_connection_info(URI("http://unknown:9999/fs"))
        # will map to default
        assert conn_info.get_jndi_properties_string() == \
            "java.naming.factory.initial#org.apache.activemq.jndi.ActiveMQInitialContextFactory;" + \
            "java.naming.provider.url#vm://localhost?broker.persistent=false"

        conn_info = hcat_service.get_jms_connection_info(URI("hcat://xyz.corp.dummy.com"))
        assert conn_info.get_jndi_properties_string() == "java.naming.factory.initial#Dummy.Factory;java.naming.provider.url#tcp:localhost:61616"
