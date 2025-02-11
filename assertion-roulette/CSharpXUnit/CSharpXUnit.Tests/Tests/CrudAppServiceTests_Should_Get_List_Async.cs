using Xunit;

namespace BackOfficeBase.Tests.Application
{
    public class CrudAppServiceTests
    {
        [Fact]
        public void Should_Get_List_Async()
        {
            {
                DefaultTestDbContext.Products.Add(new Product { Name = "E Product Name", Code = "e_product_code_for_get_list_with_filter_and_sort_async" });
                DefaultTestDbContext.Products.Add(new Product { Name = "A Product Name", Code = "a_product_code_for_get_list_with_filter_and_sort_async" });
                DefaultTestDbContext.Products.Add(new Product { Name = "B Product Name 1", Code = "b_product_code_1_for_get_list_with_filter_and_sort_async" });
                DefaultTestDbContext.Products.Add(new Product { Name = "B Product Name 1", Code = "b_product_code_2_for_get_list_with_filter_and_sort_async" });
                DefaultTestDbContext.SaveChanges();
                var pagedListInput = new PagedListInput
                {
                    Filters = new List<string>
                    {
                        "Name.Contains(\"Product\")",
                        "CreationTime > DateTime.Now.AddMinutes(-1)",
                        "Code.Contains(\"for_get_list_with_filter_and_sort_async\")"
                    },
                    Sorts = new List<string>
                    {
                        "Name",
                        "Code desc"
                    }
                };
                var pagedProductList = await _productCrudAppService.GetListAsync(pagedListInput);
                Assert.NotNull(pagedProductList);
                Assert.Equal(4, pagedProductList.TotalCount);
                Assert.Equal("b_product_code_2_for_get_list_with_filter_and_sort_async", pagedProductList.Items.ToArray()[1].Code);
            }
        }
    }
}
