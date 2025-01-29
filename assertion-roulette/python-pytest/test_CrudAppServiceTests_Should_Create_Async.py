import pytest

@pytest.mark.asyncio
async def test_should_create_async(product_crud_app_service, default_test_db_context, get_default_test_db_context):
    user_output = await product_crud_app_service.create_async({"Code": "create_async_product_code", "Name": "Create Async Product Name"})
    await default_test_db_context.save_changes_async()
    another_scope_db_context = get_default_test_db_context()
    inserted_product_dto = await another_scope_db_context.products.find_async(user_output["Id"])
    assert user_output is not None
    assert inserted_product_dto is not None
    assert user_output["Code"] == inserted_product_dto["Code"]
