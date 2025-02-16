public class DefaultServerFactoryTest
{
    [Fact]
    public async Task TestGracefulShutdown()
    {
        var objectMapper = new ObjectMapper();
        var validator = new Validator();
        var metricRegistry = new MetricRegistry();
        var environment = new Environment("test", objectMapper, validator, metricRegistry, 
                ClassLoader.GetSystemClassLoader());

        var requestReceived = new CountdownEvent(1);
        var shutdownInvoked = new CountdownEvent(1);

        environment.Jersey().Register(new TestResource(requestReceived, shutdownInvoked));

        var executor = Executors.NewScheduledThreadPool(3);
        var server = Http.Build(environment);
        
        ((AbstractNetworkConnector)server.Connectors[0]).Port = 0;

        var cleanup = executor.Schedule(() =>
        {
            if (!server.IsStopped)
            {
                server.Stop();
            }
            executor.ShutdownNow();
            return Task.CompletedTask;
        }, 5, TimeUnit.Seconds);

        await server.StartAsync();

        var port = ((AbstractNetworkConnector)server.Connectors[0]).LocalPort;

        var futureResult = executor.Submit(() =>
        {
            var url = new URL($"http://localhost:{port}/test");
            var connection = url.OpenConnection();
            connection.Connect();
            return new StreamReader(connection.GetInputStream()).ReadToEnd();
        });

        requestReceived.Wait();

        var serverStopped = executor.Submit(() =>
        {
            server.Stop();
            return Task.CompletedTask;
        });

        var connectors = server.Connectors;
        Assert.NotEmpty(connectors);
        Assert.IsType<NetworkConnector>(connectors[0]);
        var connector = (NetworkConnector)connectors[0];

        while (true)
        {
            if (!connector.IsOpen)
            {
                shutdownInvoked.Signal();
                break;
            }
            Thread.Sleep(5);
        }

        var result = await futureResult.Task;
        Assert.Equal("test", result);

        await serverStopped.Task;

        cleanup.Cancel(false);
        executor.ShutdownNow();
    }
}
