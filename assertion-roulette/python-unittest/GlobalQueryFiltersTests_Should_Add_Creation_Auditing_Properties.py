import unittest

class TestGlobalQueryFilters(unittest.TestCase):
    def test_should_add_creation_auditing_properties(self):
        result = default_test_db_context.products.add(Product(name="Product Name", code="product_code"))
        default_test_db_context.save_changes()
        
        self.assertIsNotNone(result.entity.creator_user_id)
        self.assertNotEqual("", result.entity.creator_user_id)
        self.assertEqual(datetime.today().strftime("%Y-%m-%d"), result.entity.creation_time.strftime("%Y-%m-%d"))
