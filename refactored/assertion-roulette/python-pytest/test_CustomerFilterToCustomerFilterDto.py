import pytest

class TestCustomerProfile:

    def test_customer_filter_to_customer_filter_dto(self):
        customer_filter_dto = CustomerFilterDto()
        customer_filter_dto.set_surname("Dickinson")
        customer_filter_dto.set_first_name("Bruce")
        customer_filter_dto.set_email("maiden@metal.com")
        customer_filter_dto.set_id(1)
        customer_filter_dto.set_current_page(1)
        customer_filter_dto.set_page_size(1)
        customer_filter_dto.set_order_by("desc")
        customer_filter_dto.set_sort_by("firstName")

        mapper = MapperConfiguration.create_mapper()
        customer_filter = mapper.map(customer_filter_dto, CustomerFilter)

        assert customer_filter_dto.get_first_name() == customer_filter.get_first_name(), "Explanation message"
        assert customer_filter_dto.get_surname() == customer_filter.get_surname(), "Explanation message"
        assert customer_filter_dto.get_id() == customer_filter.get_id(), "Explanation message"
        assert customer_filter_dto.get_email() == customer_filter.get_email(), "Explanation message"
        assert customer_filter_dto.get_current_page() == customer_filter.get_current_page(), "Explanation message"
        assert customer_filter_dto.get_order_by() == customer_filter.get_order_by(), "Explanation message"
        assert customer_filter_dto.get_page_size() == customer_filter.get_page_size(), "Explanation message"
        assert customer_filter_dto.get_sort_by() == customer_filter.get_sort_by(), "Explanation message"
