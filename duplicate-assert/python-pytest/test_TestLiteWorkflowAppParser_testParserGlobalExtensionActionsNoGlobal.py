import pytest

class TestLiteWorkflowAppParser:
    def test_parser_global_extension_actions_no_global(self):
        parser = LiteWorkflowAppParser(
            None,
            LiteWorkflowStoreService.LiteControlNodeHandler,
            LiteWorkflowStoreService.LiteDecisionHandler,
            LiteWorkflowStoreService.LiteActionHandler
        )

        parser.validate_and_parse(IOUtils.get_resource_as_reader("wf-schema-valid-global-ext-no-global.xml", -1), Configuration())

        with pytest.raises(WorkflowException) as excinfo:
            parser.validate_and_parse(IOUtils.get_resource_as_reader("wf-schema-invalid-global-ext-no-global.xml", -1), Configuration())
        
        assert excinfo.value.get_error_code() == ErrorCode.E0701
