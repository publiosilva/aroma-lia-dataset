import unittest

class TestCustomArgumentException(unittest.TestCase):
    @unittest.skip("")
    def test_recursive_async_exception_without_flatten_exception(self):
        recursion_count = 3

        def inner_action():
            raise Exception("Life is hard")

        def nested_func(count, action):
            if count <= 0:
                return action()
            return nested_func(count - 1, action)

        try:
            nested_func(recursion_count, inner_action)
        except Exception as ex:
            result = str(ex)
            needle_count = result.count("nested_func")
            self.assertTrue(needle_count >= recursion_count, f"{needle_count} too small")
