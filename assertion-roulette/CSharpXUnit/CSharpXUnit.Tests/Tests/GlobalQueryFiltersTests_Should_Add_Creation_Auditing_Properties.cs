using Xunit;

namespace BackOfficeBase.Tests.DataAccess
{
    public class GlobalQueryFiltersTests
    {
        [Fact]
        public void Should_Add_Creation_Auditing_Properties()
        {
            {
                var result = await DefaultTestDbContext.Products.AddAsync(new Product { Name = "Product Name", Code = "product_code" });
                await DefaultTestDbContext.SaveChangesAsync();
                Assert.NotNull(result.Entity.CreatorUserId);
                Assert.NotEqual(Guid.Empty, result.Entity.CreatorUserId);
                Assert.Equal(DateTime.Today.ToShortDateString(), result.Entity.CreationTime.ToShortDateString());
            }
        }
    }
}
