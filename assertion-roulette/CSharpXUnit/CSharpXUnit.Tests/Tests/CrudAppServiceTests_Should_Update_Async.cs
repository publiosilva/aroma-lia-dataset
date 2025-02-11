using Xunit;

namespace BackOfficeBase.Tests.Application
{
    public class CrudAppServiceTests
    {
        [Fact]
        public void Should_Update_Async()
        {
            {
                var dbContextForAddEntity = GetDefaultTestDbContext();
                var productDto = await dbContextForAddEntity.Products.AddAsync(new Product { Code = "update_product_code", Name = "Update Product Name" });
                await dbContextForAddEntity.SaveChangesAsync();
                var userOutput = _productCrudAppService.Update(new UpdateProductInput { Id = productDto.Entity.Id, Code = "update_product_code_updated", Name = "Update Product Name Updated" });
                await DefaultTestDbContext.SaveChangesAsync();
                var dbContextForGetEntity = GetDefaultTestDbContext();
                var updatedProductDto = await dbContextForGetEntity.Products.FindAsync(productDto.Entity.Id);
                Assert.NotNull(userOutput);
                Assert.NotNull(productDto);
                Assert.NotNull(updatedProductDto);
                Assert.Equal("update_product_code_updated", updatedProductDto.Code);
                Assert.Equal("Update Product Name Updated", updatedProductDto.Name);
            }
        }
    }
}
