import pytest

class TestCustomerProfile:
    def test_customer_filter_to_customer_filter_dto(self):
        customer_filter_dto = {
            "surname": "Dickinson",
            "first_name": "Bruce",
            "email": "maiden@metal.com",
            "id": 1,
            "current_page": 1,
            "page_size": 1,
            "order_by": "desc",
            "sort_by": "firstName"
        }
        mapper = MapperConfiguration.create_mapper()
        customer_filter = mapper.map(CustomerFilter, customer_filter_dto)
        assert customer_filter_dto['first_name'] == customer_filter.first_name
        assert customer_filter_dto['surname'] == customer_filter.surname
        assert customer_filter_dto['id'] == customer_filter.id
        assert customer_filter_dto['email'] == customer_filter.email
        assert customer_filter_dto['current_page'] == customer_filter.current_page
        assert customer_filter_dto['order_by'] == customer_filter.order_by
        assert customer_filter_dto['page_size'] == customer_filter.page_size
        assert customer_filter_dto['sort_by'] == customer_filter.sort_by
