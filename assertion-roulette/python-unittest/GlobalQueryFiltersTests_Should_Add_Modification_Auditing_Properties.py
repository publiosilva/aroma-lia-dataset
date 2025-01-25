import unittest

class TestGlobalQueryFilters(unittest.TestCase):
    def test_should_add_modification_auditing_properties(self):
        result = default_test_db_context.products.add(Product(name="Product Name", code="product_code"))
        default_test_db_context.save_changes()
        
        default_test_db_context.products.update(result.entity)
        default_test_db_context.save_changes()
        
        db_context_to_get_updated_entity = get_default_test_db_context()
        updated_entity = db_context_to_get_updated_entity.products.find(result.entity.id)
        
        self.assertIsNotNone(updated_entity.modifier_user_id)
        self.assertNotEqual("", updated_entity.modifier_user_id)
        self.assertIsNotNone(updated_entity.modification_time)
        self.assertEqual(datetime.today().strftime("%Y-%m-%d"), updated_entity.modification_time.strftime("%Y-%m-%d"))
