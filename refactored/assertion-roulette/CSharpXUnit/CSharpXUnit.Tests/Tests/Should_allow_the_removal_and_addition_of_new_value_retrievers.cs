using Xunit;

public class IExistsForTestingValueRetrieving 
{
    [Fact]
    public void shouldAllowTheRemovalAndAdditionOfNewValueRetrievers() 
    {
        {
            var service = new Service();
            foreach (var valueRetriever in service.GetValueRetrievers().ToArray())
            {
                service.GetValueRetrievers().Unregister(valueRetriever);
                Assert.False(service.GetValueRetrievers().Contains(valueRetriever), "Explanation message");
            }

            var thing = new IExistsForTestingValueRetrieving();
            service.GetValueRetrievers().Register(thing);
            Assert.Equal(1, service.GetValueRetrievers().Size(), "Explanation message");
            Assert.Same(thing, service.GetValueRetrievers().Get(0), "Explanation message");
        }
    }
}
