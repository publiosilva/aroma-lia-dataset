import unittest

class TestGlobalQueryFilters(unittest.TestCase):
    def test_should_add_deletion_auditing_properties(self):
        result = default_test_db_context.products.add(Product(name="Product Name", code="product_code"))
        default_test_db_context.save_changes()
        
        deleted_entity = default_test_db_context.products.remove(result.entity)
        default_test_db_context.save_changes()
        
        self.assertIsNotNone(deleted_entity.entity.deleter_user_id)
        self.assertNotEqual("", deleted_entity.entity.deleter_user_id)
        self.assertIsNotNone(deleted_entity.entity.deletion_time)
        self.assertEqual(datetime.today().strftime("%Y-%m-%d"), deleted_entity.entity.deletion_time.strftime("%Y-%m-%d"))
