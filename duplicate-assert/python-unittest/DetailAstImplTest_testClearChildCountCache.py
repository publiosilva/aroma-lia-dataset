import unittest

class DetailAstImplTest(unittest.TestCase):

    def test_clear_child_count_cache(self):
        parent = DetailAstImpl()
        child = DetailAstImpl()
        parent.set_first_child(child)

        clear_child_count_cache_methods = [
            child.set_next_sibling,
            child.add_previous_sibling,
            child.add_next_sibling
        ]

        for method in clear_child_count_cache_methods:
            start_count = parent.get_child_count()
            method(None)
            intermediate_count = Whitebox.get_internal_state(parent, "childCount")
            finish_count = parent.get_child_count()
            self.assertEqual(start_count, finish_count, "Child count has changed")
            self.assertEqual(float('-inf'), intermediate_count, "Invalid child count")

        start_count = child.get_child_count()
        child.add_child(None)
        intermediate_count = Whitebox.get_internal_state(child, "childCount")
        finish_count = child.get_child_count()
        self.assertEqual(start_count, finish_count, "Child count has changed")
        self.assertEqual(float('-inf'), intermediate_count, "Invalid child count")
