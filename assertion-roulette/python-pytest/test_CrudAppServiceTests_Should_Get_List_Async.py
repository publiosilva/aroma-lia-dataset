import pytest

@pytest.mark.asyncio
async def test_should_get_list_async(default_test_db_context, product_crud_app_service):
    default_test_db_context.products.add({"Name": "E Product Name", "Code": "e_product_code_for_get_list_with_filter_and_sort_async"})
    default_test_db_context.products.add({"Name": "A Product Name", "Code": "a_product_code_for_get_list_with_filter_and_sort_async"})
    default_test_db_context.products.add({"Name": "B Product Name 1", "Code": "b_product_code_1_for_get_list_with_filter_and_sort_async"})
    default_test_db_context.products.add({"Name": "B Product Name 1", "Code": "b_product_code_2_for_get_list_with_filter_and_sort_async"})
    await default_test_db_context.save_changes_async()
    paged_list_input = {
        "Filters": [
            'Name.Contains("Product")',
            "CreationTime > DateTime.Now.AddMinutes(-1)",
            'Code.Contains("for_get_list_with_filter_and_sort_async")'
        ],
        "Sorts": [
            "Name",
            "Code desc"
        ]
    }
    paged_product_list = await product_crud_app_service.get_list_async(paged_list_input)
    assert paged_product_list is not None
    assert paged_product_list["TotalCount"] == 4
    assert paged_product_list["Items"][1]["Code"] == "b_product_code_2_for_get_list_with_filter_and_sort_async"
