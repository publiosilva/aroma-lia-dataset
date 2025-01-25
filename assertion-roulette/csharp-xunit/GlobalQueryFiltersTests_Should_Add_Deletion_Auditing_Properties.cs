using Xunit;

namespace BackOfficeBase.Tests.DataAccess
{
    public class GlobalQueryFiltersTests
    {
        [Fact]
        public void Should_Add_Deletion_Auditing_Properties()
        {
            {
                var result = await DefaultTestDbContext.Products.AddAsync(new Product { Name = "Product Name", Code = "product_code" });
                await DefaultTestDbContext.SaveChangesAsync();
                var deletedEntity = DefaultTestDbContext.Products.Remove(result.Entity);
                await DefaultTestDbContext.SaveChangesAsync();
                Assert.NotNull(deletedEntity.Entity.DeleterUserId);
                Assert.NotEqual(Guid.Empty, deletedEntity.Entity.DeleterUserId);
                Assert.NotNull(deletedEntity.Entity.DeletionTime);
                Assert.Equal(DateTime.Today.ToShortDateString(), deletedEntity.Entity.DeletionTime.Value.ToShortDateString());
            }
        }
    }
}
