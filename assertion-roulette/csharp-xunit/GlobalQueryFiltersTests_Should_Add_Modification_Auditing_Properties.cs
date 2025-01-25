using Xunit;

namespace BackOfficeBase.Tests.DataAccess
{
    public class GlobalQueryFiltersTests
    {
        [Fact]
        public void Should_Add_Modification_Auditing_Properties()
        {
            {
                var result = await DefaultTestDbContext.Products.AddAsync(new Product { Name = "Product Name", Code = "product_code" });
                await DefaultTestDbContext.SaveChangesAsync();
                DefaultTestDbContext.Products.Update(result.Entity);
                await DefaultTestDbContext.SaveChangesAsync();
                var dbContextToGetUpdatedEntity = GetDefaultTestDbContext();
                var updatedEntity = await dbContextToGetUpdatedEntity.Products.FindAsync(result.Entity.Id);
                Assert.NotNull(updatedEntity.ModifierUserId);
                Assert.NotEqual(Guid.Empty, updatedEntity.ModifierUserId);
                Assert.NotNull(updatedEntity.ModificationTime);
                Assert.Equal(DateTime.Today.ToShortDateString(), updatedEntity.ModificationTime.Value.ToShortDateString());
            }
        }
    }
}
