import pytest
from datetime import datetime

@pytest.mark.asyncio
async def test_should_add_deletion_auditing_properties(default_test_db_context):
    result = await default_test_db_context.products.add_async({"Name": "Product Name", "Code": "product_code"})
    await default_test_db_context.save_changes_async()
    deleted_entity = default_test_db_context.products.remove(result.entity)
    await default_test_db_context.save_changes_async()
    assert deleted_entity.entity["DeleterUserId"] is not None
    assert deleted_entity.entity["DeleterUserId"] != "00000000-0000-0000-0000-000000000000"
    assert deleted_entity.entity["DeletionTime"] is not None
    assert datetime.today().strftime("%Y-%m-%d") == deleted_entity.entity["DeletionTime"].strftime("%Y-%m-%d")
