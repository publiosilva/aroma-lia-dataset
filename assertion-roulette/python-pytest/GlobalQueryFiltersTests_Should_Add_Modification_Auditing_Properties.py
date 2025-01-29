import pytest
from datetime import datetime

@pytest.mark.asyncio
async def test_should_add_modification_auditing_properties(default_test_db_context, get_default_test_db_context):
    result = await default_test_db_context.products.add_async({"Name": "Product Name", "Code": "product_code"})
    await default_test_db_context.save_changes_async()
    default_test_db_context.products.update(result.entity)
    await default_test_db_context.save_changes_async()
    db_context_to_get_updated_entity = get_default_test_db_context()
    updated_entity = await db_context_to_get_updated_entity.products.find_async(result.entity["Id"])
    assert updated_entity["ModifierUserId"] is not None
    assert updated_entity["ModifierUserId"] != "00000000-0000-0000-0000-000000000000"
    assert updated_entity["ModificationTime"] is not None
    assert datetime.today().strftime("%Y-%m-%d") == updated_entity["ModificationTime"].strftime("%Y-%m-%d")
