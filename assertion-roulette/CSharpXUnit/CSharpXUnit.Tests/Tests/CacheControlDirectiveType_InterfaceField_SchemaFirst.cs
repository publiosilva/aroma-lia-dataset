using Xunit;

namespace DefaultNamespace
{
    public class CacheControlDirectiveTypeTests
    {
        [Fact]
        public void CacheControlDirectiveType_InterfaceField_SchemaFirst()
        {
            {
                var schema = SchemaBuilder.New().AddDocumentFromString(@"
                        type Query {
                            field: InterfaceType
                        }
            
                        interface InterfaceType {
                            field: String @cacheControl(maxAge: 500 scope: PRIVATE inheritMaxAge: true)
                        }
            
                        type ObjectType implements InterfaceType {
                            field: String
                        }
                        ").AddDirectiveType<CacheControlDirectiveType>().Use(_ => _).Create();
                var type = schema.GetType<InterfaceType>("InterfaceType");
                var directive = type.Fields["field"].Directives.Single(d => d.Type.Name == "cacheControl");
                var obj = directive.AsValue<CacheControlDirective>();
                Assert.Equal(500, obj.MaxAge);
                Assert.Equal(CacheControlScope.Private, obj.Scope);
                Assert.Equal(true, obj.InheritMaxAge);
            }
        }
    }
}
