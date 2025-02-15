import static org.junit.jupiter.api.Assertions.assertEquals;
import org.junit.jupiter.api.Test;

public class CacheControlDirectiveTypeTests {

    @Test
    public void cacheControlDirectiveType_InterfaceField_SchemaFirst() {
        {
            var schema = SchemaBuilder.new().addDocumentFromString("""
                    type Query {
                        field: InterfaceType
                    }

                    interface InterfaceType {
                        field: String @cacheControl(maxAge: 500 scope: PRIVATE inheritMaxAge: true)
                    }

                    type ObjectType implements InterfaceType {
                        field: String
                    }
                    """).addDirectiveType(CacheControlDirectiveType.class).use(_ -> _).create();
            var type = schema.getType("InterfaceType", InterfaceType.class);
            var directive = type.getFields().get("field").getDirectives().stream()
                    .filter(d -> d.getType().getName().equals("cacheControl")).findSingle().orElse(null);
            var obj = directive.asValue(CacheControlDirective.class);
            assertEquals(500, obj.getMaxAge());
            assertEquals(CacheControlScope.Private, obj.getScope());
            assertEquals(true, obj.isInheritMaxAge());
        }
    }
}
