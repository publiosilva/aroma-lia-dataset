import unittest

class TestCrudAppService(unittest.TestCase):
    def test_should_update(self):
        db_context_for_add_entity = get_default_test_db_context()
        product_dto = db_context_for_add_entity.products.add(Product(code="update_product_code", name="Update Product Name"))
        db_context_for_add_entity.save_changes()
        
        user_output = _product_crud_app_service.update(UpdateProductInput(id=product_dto.entity.id, code="update_product_code_updated", name="Update Product Name Updated"))
        default_test_db_context.save_changes()
        
        db_context_for_get_entity = get_default_test_db_context()
        updated_product_dto = db_context_for_get_entity.products.find(product_dto.entity.id)
        
        self.assertIsNotNone(user_output)
        self.assertIsNotNone(product_dto)
        self.assertIsNotNone(updated_product_dto)
        self.assertEqual("update_product_code_updated", updated_product_dto.code)
        self.assertEqual("Update Product Name Updated", updated_product_dto.name)
