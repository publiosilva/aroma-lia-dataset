public class DefaultServerFactoryTest {
    @Test
    public void testGracefulShutdown() throws Exception {
        ObjectMapper objectMapper = Jackson.newObjectMapper();
        Validator validator = Validation.buildDefaultValidatorFactory().getValidator();
        MetricRegistry metricRegistry = new MetricRegistry();
        Environment environment = new Environment("test", objectMapper, validator, metricRegistry,
                ClassLoader.getSystemClassLoader());

        CountDownLatch requestReceived = new CountDownLatch(1);
        CountDownLatch shutdownInvoked = new CountDownLatch(1);

        environment.jersey().register(new TestResource(requestReceived, shutdownInvoked));

        final ScheduledExecutorService executor = Executors.newScheduledThreadPool(3);
        final Server server = http.build(environment);
        
        ((AbstractNetworkConnector)server.getConnectors()[0]).setPort(0);

        ScheduledFuture<Void> cleanup = executor.schedule(new Callable<Void>() {
            @Override
            public Void call() throws Exception {
                if (!server.isStopped()) {
                    server.stop();
                }
                executor.shutdownNow();
                return null;
            }
        }, 5, TimeUnit.SECONDS);


        server.start();

        final int port = ((AbstractNetworkConnector) server.getConnectors()[0]).getLocalPort();

        Future<String> futureResult = executor.submit(new Callable<String>() {
            @Override
            public String call() throws Exception {
                URL url = new URL("http://localhost:" + port + "/test");
                URLConnection connection = url.openConnection();
                connection.connect();
                return CharStreams.toString(new InputStreamReader(connection.getInputStream()));
            }
        });

        requestReceived.await();

        Future<Void> serverStopped = executor.submit(new Callable<Void>() {
            @Override
            public Void call() throws Exception {
                server.stop();
                return null;
            }
        });

        Connector[] connectors = server.getConnectors();
        assertThat(connectors).isNotEmpty();
        assertThat(connectors[0]).isInstanceOf(NetworkConnector.class);
        NetworkConnector connector = (NetworkConnector) connectors[0];

        // wait for server to close the connectors
        while (true) {
            if (!connector.isOpen()) {
                shutdownInvoked.countDown();
                break;
            }
            // Thread.sleep(5);
        }

        String result = futureResult.get();
        assertThat(result).isEqualTo("test");

        serverStopped.get();

        // cancel the cleanup future since everything succeeded
        cleanup.cancel(false);
        executor.shutdownNow();
    }
}
