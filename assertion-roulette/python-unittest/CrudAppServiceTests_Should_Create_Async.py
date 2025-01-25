import unittest

class TestCrudAppService(unittest.TestCase):
    def test_should_create(self):
        user_output = _product_crud_app_service.create(CreateProductInput(code="create_async_product_code", name="Create Async Product Name"))
        default_test_db_context.save_changes()
        another_scope_db_context = get_default_test_db_context()
        inserted_product_dto = another_scope_db_context.products.find(user_output.id)
        self.assertIsNotNone(user_output)
        self.assertIsNotNone(inserted_product_dto)
        self.assertEqual(user_output.code, inserted_product_dto.code)
