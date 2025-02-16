import pytest
from your_module import HCatAccessorService  # Replace with the actual module name

class TestHCatAccessorService:

    def test_get_jms_connection_info_no_default(self):
        services.destroy()
        services = super().setup_services_for_hcatalog()
        conf = services.get_conf()
        server2 = "hcat://${1}.${2}.server.com:8020=java.naming.factory.initial#Dummy.Factory;" \
                   "java.naming.provider.url#tcp://broker.${2}:61616"
        server3 = "hcat://xyz.corp.dummy.com=java.naming.factory.initial#Dummy.Factory;" \
                   "java.naming.provider.url#tcp:localhost:61616"

        jms_connection_url = server2 + "," + server3
        conf.set(HCatAccessorService.JMS_CONNECTIONS_PROPERTIES, jms_connection_url)
        services.init()

        hcat_service = services.get(HCatAccessorService)
        conn_info = hcat_service.get_jms_connection_info(URI("http://unknown:9999/fs"))
        assert conn_info is None
        conn_info = hcat_service.get_jms_connection_info(URI("hcat://server1.colo1.server.com:8020/db/table/pk1=val1;pk2=val2"))
        assert conn_info.get_jndi_properties_string() == "java.naming.factory.initial#Dummy.Factory;java.naming.provider.url#tcp://broker.colo1:61616"
        conn_info = hcat_service.get_jms_connection_info(URI("hcat://xyz.corp.dummy.com/db/table"))
        assert conn_info.get_jndi_properties_string() == "java.naming.factory.initial#Dummy.Factory;java.naming.provider.url#tcp:localhost:61616"
