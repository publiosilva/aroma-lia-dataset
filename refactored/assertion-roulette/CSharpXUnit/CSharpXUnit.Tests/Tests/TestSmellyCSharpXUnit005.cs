using Xunit;

namespace DefaultNamespace
{
    public class ComponentRenderingTest
    {
        [Fact]
        public void CanUseAddMultipleAttributes1()
        {
            {
                var cut = RenderComponent<DuplicateAttributesComponent>();
                var element = cut.Find("#duplicate-on-element > div");
                Assert.True(element.HasAttribute("bool")); // attribute is present
            }
        }

        [Fact]
        public void CanUseAddMultipleAttributes2()
        {
            {
                var cut = RenderComponent<DuplicateAttributesComponent>();
                var element = cut.Find("#duplicate-on-element > div");
                Assert.Equal("middle-value", element.GetAttribute("string"));
            }
        }

        [Fact]
        public void CanUseAddMultipleAttributes3()
        {
            {
                var cut = RenderComponent<DuplicateAttributesComponent>();
                var element = cut.Find("#duplicate-on-element > div");
                Assert.Equal("unmatched-value", element.GetAttribute("unmatched"));
            }
        }

        [Fact]
        public void CanUseAddMultipleAttributes4()
        {
            {
                var cut = RenderComponent<DuplicateAttributesComponent>();
                var element = cut.Find("#duplicate-on-element > div");
                element = cut.Find("#duplicate-on-element-override > div");
                Assert.False(element.HasAttribute("bool")); // attribute is not present
            }
        }

        [Fact]
        public void CanUseAddMultipleAttributes5()
        {
            {
                var cut = RenderComponent<DuplicateAttributesComponent>();
                var element = cut.Find("#duplicate-on-element > div");
                element = cut.Find("#duplicate-on-element-override > div");
                Assert.Equal("other-text", element.GetAttribute("string"));
            }
        }

        [Fact]
        public void CanUseAddMultipleAttributes6()
        {
            {
                var cut = RenderComponent<DuplicateAttributesComponent>();
                var element = cut.Find("#duplicate-on-element > div");
                element = cut.Find("#duplicate-on-element-override > div");
                Assert.Equal("unmatched-value", element.GetAttribute("unmatched"));
            }
        }
    }
}
