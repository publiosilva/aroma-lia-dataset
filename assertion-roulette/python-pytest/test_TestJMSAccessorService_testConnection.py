import pytest

def test_connection(services):
    hcat_service = services.get(HCatAccessorService)
    jms_service = services.get(JMSAccessorService)
    conn_info = hcat_service.get_jms_connection_info("hcat://hcatserver.blue.server.com:8020")
    ctxt1 = jms_service.create_connection_context(conn_info)
    assert ctxt1.is_connection_initialized()
    conn_info1 = hcat_service.get_jms_connection_info("http://unknown:80")
    ctxt2 = jms_service.create_connection_context(conn_info1)
    assert ctxt2.is_connection_initialized()
    assert ctxt1 == ctxt2
    ctxt1.close()
