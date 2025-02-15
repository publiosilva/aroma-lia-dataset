import org.junit.Test;
import org.junit.Ignore;
import static org.junit.Assert.*;

public class AutoMapperConfigurationTests {

    @Test
    // @Ignore("")
    public void configuration_IsValid() {
        MapperConfiguration config = new MapperConfiguration(cfg -> cfg.addProfile(new DefaultProfile()));
        config.assertConfigurationIsValid();
    }
}
