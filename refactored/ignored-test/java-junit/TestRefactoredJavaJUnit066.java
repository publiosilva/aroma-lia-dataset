import org.junit.Test;
import static org.junit.Assert.assertEquals;
import org.junit.Ignore;

public class StringUtilsTests {
    
    @Test
    // @Ignore("Skipping test")
    public void formatArgumentsTest() {
        ProcessBuilder p = new ProcessBuilder();
        p.redirectErrorStream(true);
        p.command("/bin/echo", "-n", "foo", "'", "bar");
        
        try {
            Process process = p.start();
            String output = new BufferedReader(new InputStreamReader(process.getInputStream())).lines()
                    .collect(Collectors.joining("\n"));
            assertEquals("foo ' bar", output);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
