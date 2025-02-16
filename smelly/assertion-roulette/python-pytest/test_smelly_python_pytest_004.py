import pytest

class TestCacheControlDirectiveType:
    def test_cache_control_directive_type_interface_field_schema_first(self):
        schema = SchemaBuilder.new().add_document_from_string("""
                type Query {
                    field: InterfaceType
                }

                interface InterfaceType {
                    field: String @cacheControl(maxAge: 500 scope: PRIVATE inheritMaxAge: true)
                }

                type ObjectType implements InterfaceType {
                    field: String
                }
                """).add_directive_type(CacheControlDirectiveType).use(lambda _: _).create()
        type = schema.get_type("InterfaceType")
        directive = next(d for d in type.fields["field"].directives if d.type.name == "cacheControl")
        obj = directive.as_value()
        assert obj.max_age == 500
        assert obj.scope == CacheControlScope.Private
        assert obj.inherit_max_age is True
