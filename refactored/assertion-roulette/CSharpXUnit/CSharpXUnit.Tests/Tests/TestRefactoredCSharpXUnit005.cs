using Xunit;

public class ComponentRenderingTest
{
    [Fact]
    public void CanUseAddMultipleAttributes()
    {
        {
            var cut = RenderComponent(DuplicateAttributesComponent.GetType());
            var element = cut.Find("#duplicate-on-element > div");
            Assert.True(element.HasAttribute("bool"), "Explanation message"); // attribute is present
            Assert.Equal("middle-value", element.GetAttribute("string"), "Explanation message");
            Assert.Equal("unmatched-value", element.GetAttribute("unmatched"), "Explanation message");
            element = cut.Find("#duplicate-on-element-override > div");
            Assert.False(element.HasAttribute("bool"), "Explanation message"); // attribute is not present
            Assert.Equal("other-text", element.GetAttribute("string"), "Explanation message");
            Assert.Equal("unmatched-value", element.GetAttribute("unmatched"), "Explanation message");
        }
    }
}
