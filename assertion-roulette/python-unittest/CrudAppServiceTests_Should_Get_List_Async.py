import unittest

class TestCrudAppService(unittest.TestCase):
    def test_should_get_list(self):
        default_test_db_context.products.add(Product(name="E Product Name", code="e_product_code_for_get_list_with_filter_and_sort_async"))
        default_test_db_context.products.add(Product(name="A Product Name", code="a_product_code_for_get_list_with_filter_and_sort_async"))
        default_test_db_context.products.add(Product(name="B Product Name 1", code="b_product_code_1_for_get_list_with_filter_and_sort_async"))
        default_test_db_context.products.add(Product(name="B Product Name 1", code="b_product_code_2_for_get_list_with_filter_and_sort_async"))
        default_test_db_context.save_changes()
        
        paged_list_input = PagedListInput(
            filters=[
                'Name.Contains("Product")',
                'CreationTime > DateTime.now().add_minutes(-1)',
                'Code.Contains("for_get_list_with_filter_and_sort_async")'
            ],
            sorts=[
                "Name",
                "Code desc"
            ]
        )
        
        paged_product_list = _product_crud_app_service.get_list(paged_list_input)
        
        self.assertIsNotNone(paged_product_list)
        self.assertEqual(4, paged_product_list.total_count)
        self.assertEqual("b_product_code_2_for_get_list_with_filter_and_sort_async", paged_product_list.items[1].code)
