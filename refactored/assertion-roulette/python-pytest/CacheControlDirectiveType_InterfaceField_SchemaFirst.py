import pytest

class TestCacheControlDirectiveType:

    def test_cache_control_directive_type_interface_field_schema_first(self):
        schema = SchemaBuilder.build().add_document_from_string("type Query {\n" +
            "    field: InterfaceType\n" +
            "}\n\n" +
            "interface InterfaceType {\n" +
            "    field: String @cacheControl(maxAge: 500 scope: PRIVATE inheritMaxAge: true)\n" +
            "}\n\n" +
            "type ObjectType implements InterfaceType {\n" +
            "    field: String\n" +
            "}").add_directive_type(CacheControlDirectiveType).use(lambda _: _).create()
        
        type = schema.get_type("InterfaceType", InterfaceType)
        directive = next((d for d in type.get_fields()["field"].get_directives() 
                          if d.get_type().get_name() == "cacheControl"), None)
        obj = directive.as_value(CacheControlDirective)
        
        assert obj.get_max_age() == 500, "Explanation message"
        assert obj.get_scope() == CacheControlScope.Private, "Explanation message"
        assert obj.is_inherit_max_age() is True, "Explanation message"
