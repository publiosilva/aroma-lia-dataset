using Xunit;

namespace BackOfficeBase.Tests.Application
{
    public class CrudAppServiceTests
    {
        [Fact]
        public void Should_Create_Async()
        {
            {
                var userOutput = await _productCrudAppService.CreateAsync(new CreateProductInput { Code = "create_async_product_code", Name = "Create Async Product Name" });
                await DefaultTestDbContext.SaveChangesAsync();
                var anotherScopeDbContext = GetDefaultTestDbContext();
                var insertedProductDto = await anotherScopeDbContext.Products.FindAsync(userOutput.Id);
                Assert.NotNull(userOutput);
                Assert.NotNull(insertedProductDto);
                Assert.Equal(userOutput.Code, insertedProductDto.Code);
            }
        }
    }
}
