using Xunit;

namespace DefaultNamespace
{
    public class CacheControlDirectiveTypeTests
    {
        [Fact]
        public void CacheControlDirectiveType_InterfaceField_SchemaFirst1()
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
            }
        }

        [Fact]
        public void CacheControlDirectiveType_InterfaceField_SchemaFirst2()
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
                Assert.Equal(CacheControlScope.Private, obj.Scope);
            }
        }

        [Fact]
        public void CacheControlDirectiveType_InterfaceField_SchemaFirst3()
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
                Assert.Equal(true, obj.InheritMaxAge);
            }
        }
    }
}
