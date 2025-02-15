public class TestLiteWorkflowAppParser
{
    [Fact]
    public void TestParserGlobalExtensionActionsNoGlobal()
    {
        var parser = new LiteWorkflowAppParser(null,
            typeof(LiteWorkflowStoreService.LiteControlNodeHandler),
            typeof(LiteWorkflowStoreService.LiteDecisionHandler),
            typeof(LiteWorkflowStoreService.LiteActionHandler));

        parser.ValidateAndParse(IOUtils.GetResourceAsReader("wf-schema-valid-global-ext-no-global.xml", -1), new Configuration());

        Exception exception = Record.Exception(() =>
        {
            parser.ValidateAndParse(IOUtils.GetResourceAsReader("wf-schema-invalid-global-ext-no-global.xml", -1),
                new Configuration());
        });

        Assert.IsType<WorkflowException>(exception);
        Assert.Equal(ErrorCode.E0701, ((WorkflowException)exception).ErrorCode);
    }
}
