using Xunit;

public class CacheControlDirectiveTypeTests
{
    [Fact]
    public void CacheControlDirectiveType_InterfaceField_SchemaFirst()
    {
        var schema = SchemaBuilder.Build().AddDocumentFromString("type Query {\n" +
            "    field: InterfaceType\n" +
            "}\n\n" +
            "interface InterfaceType {\n" +
            "    field: String @cacheControl(maxAge: 500 scope: PRIVATE inheritMaxAge: true)\n" +
            "}\n\n" +
            "type ObjectType implements InterfaceType {\n" +
            "    field: String\n" +
            "}").AddDirectiveType(typeof(CacheControlDirectiveType)).Use(_ => _).Create();
        var type = schema.GetType("InterfaceType", typeof(InterfaceType));
        var directive = type.GetFields()["field"].GetDirectives().FirstOrDefault(d => d.GetType().Name.Equals("cacheControl"));
        var obj = directive.AsValue(typeof(CacheControlDirective));
        Assert.Equal(500, obj.MaxAge, "Explanation message");
        Assert.Equal(CacheControlScope.Private, obj.Scope, "Explanation message");
        Assert.True(obj.InheritMaxAge, "Explanation message");
    }
}
