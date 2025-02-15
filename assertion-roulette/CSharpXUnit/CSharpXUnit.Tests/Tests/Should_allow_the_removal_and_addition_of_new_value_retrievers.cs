using Xunit;

namespace DefaultNamespace
{
    public class IExistsForTestingValueRetrieving
    {
        [Fact]
        public void Should_allow_the_removal_and_addition_of_new_value_retrievers()
        {
            {
                var service = new Service();
                foreach (var valueRetriever in service.ValueRetrievers.ToArray())
                {
                    service.ValueRetrievers.Unregister(valueRetriever);
                    Assert.DoesNotContain(valueRetriever, service.ValueRetrievers);
                }
            
                var thing = new IExistsForTestingValueRetrieving();
                service.ValueRetrievers.Register(thing);
                Assert.Single(service.ValueRetrievers);
                Assert.Same(thing, service.ValueRetrievers.First());
            }
        }
    }
}
