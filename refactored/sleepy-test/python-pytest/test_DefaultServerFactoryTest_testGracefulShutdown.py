import pytest
from concurrent.futures import ThreadPoolExecutor, Future, wait, ALL_COMPLETED
from time import sleep
from urllib import request

def test_graceful_shutdown():
    object_mapper = Jackson.new_objectMapper()
    validator = Validation.buildDefaultValidatorFactory().getValidator()
    metric_registry = MetricRegistry()
    environment = Environment("test", object_mapper, validator, metric_registry, 
                              ClassLoader.getSystemClassLoader())

    request_received = threading.Event()
    shutdown_invoked = threading.Event()

    environment.jersey().register(TestResource(request_received, shutdown_invoked))

    executor = ThreadPoolExecutor(max_workers=3)
    server = http.build(environment)
    
    connector = server.getConnectors()[0]
    connector.setPort(0)

    cleanup_future = executor.submit(lambda: (server.stop() if not server.isStopped() else None))

    server.start()

    port = connector.getLocalPort()

    future_result = executor.submit(lambda: request.urlopen(f"http://localhost:{port}/test").read().decode())

    request_received.wait()

    server_stopped = executor.submit(lambda: server.stop())

    connectors = server.getConnectors()
    assert connectors
    assert isinstance(connectors[0], NetworkConnector)
    connector = connectors[0]

    while True:
        if not connector.isOpen():
            shutdown_invoked.set()
            break
        # sleep(0.005)

    result = future_result.result()
    assert result == "test"

    server_stopped.result()

    cleanup_future.cancel()
    executor.shutdown(wait=True)
