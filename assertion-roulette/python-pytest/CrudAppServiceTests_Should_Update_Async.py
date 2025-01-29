import pytest

@pytest.mark.asyncio
async def test_should_update_async(get_default_test_db_context, product_crud_app_service, default_test_db_context):
    db_context_for_add_entity = get_default_test_db_context()
    product_dto = await db_context_for_add_entity.products.add_async({"Code": "update_product_code", "Name": "Update Product Name"})
    await db_context_for_add_entity.save_changes_async()
    user_output = await product_crud_app_service.update({"Id": product_dto.entity.id, "Code": "update_product_code_updated", "Name": "Update Product Name Updated"})
    await default_test_db_context.save_changes_async()
    db_context_for_get_entity = get_default_test_db_context()
    updated_product_dto = await db_context_for_get_entity.products.find_async(product_dto.entity.id)
    assert user_output is not None
    assert product_dto is not None
    assert updated_product_dto is not None
    assert updated_product_dto["Code"] == "update_product_code_updated"
    assert updated_product_dto["Name"] == "Update Product Name Updated"
