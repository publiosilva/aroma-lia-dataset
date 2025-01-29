import pytest
from datetime import datetime

@pytest.mark.asyncio
async def test_should_add_creation_auditing_properties(default_test_db_context):
    result = await default_test_db_context.products.add_async({"Name": "Product Name", "Code": "product_code"})
    await default_test_db_context.save_changes_async()
    assert result.entity["CreatorUserId"] is not None
    assert result.entity["CreatorUserId"] != "00000000-0000-0000-0000-000000000000"
    assert datetime.today().strftime("%Y-%m-%d") == result.entity["CreationTime"].strftime("%Y-%m-%d")
