using Xunit;

namespace DefaultNamespace
{
    public class IExistsForTestingValueRetrieving
    {
        [Fact]
        public void Should_allow_the_removal_and_addition_of_new_value_retrievers1()
        {
            {
                var service = new Service();
                foreach (var valueRetriever in service.ValueRetrievers.ToArray())
                {
                    service.ValueRetrievers.Unregister(valueRetriever);
                    Assert.DoesNotContain(valueRetriever, service.ValueRetrievers);
                }
            }
        }

        [Fact]
        public void Should_allow_the_removal_and_addition_of_new_value_retrievers2()
        {
            {
                var service = new Service();
                foreach (var valueRetriever in service.ValueRetrievers.ToArray())
                {
                    service.ValueRetrievers.Unregister(valueRetriever);
                }
            
                var thing = new IExistsForTestingValueRetrieving();
                service.ValueRetrievers.Register(thing);
                Assert.Single(service.ValueRetrievers);
            }
        }

        [Fact]
        public void Should_allow_the_removal_and_addition_of_new_value_retrievers3()
        {
            {
                var service = new Service();
                foreach (var valueRetriever in service.ValueRetrievers.ToArray())
                {
                    service.ValueRetrievers.Unregister(valueRetriever);
                }
            
                var thing = new IExistsForTestingValueRetrieving();
                service.ValueRetrievers.Register(thing);
                Assert.Same(thing, service.ValueRetrievers.First());
            }
        }
    }
}
