using Xunit;

namespace DefaultNamespace
{
    public class ComponentRenderingTest
    {
        [Fact]
        public void CanUseAddMultipleAttributes()
        {
            {
                var cut = RenderComponent<DuplicateAttributesComponent>();
                var element = cut.Find("#duplicate-on-element > div");
                Assert.True(element.HasAttribute("bool")); // attribute is present
                Assert.Equal("middle-value", element.GetAttribute("string"));
                Assert.Equal("unmatched-value", element.GetAttribute("unmatched"));
                element = cut.Find("#duplicate-on-element-override > div");
                Assert.False(element.HasAttribute("bool")); // attribute is not present
                Assert.Equal("other-text", element.GetAttribute("string"));
                Assert.Equal("unmatched-value", element.GetAttribute("unmatched"));
            }
        }
    }
}
