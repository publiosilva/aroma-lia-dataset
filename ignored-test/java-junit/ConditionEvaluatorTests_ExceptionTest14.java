import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.Disabled;
import static org.junit.jupiter.api.Assertions.assertEquals;

import java.io.*;

public class ConditionEvaluatorTests {

    @Test
    @Disabled("")
    public void exceptionTest14() throws Exception {
        InvalidOperationException inner = new InvalidOperationException("f");
        ConditionParseException ex1 = new ConditionParseException("msg", inner);
        ByteArrayOutputStream bos = new ByteArrayOutputStream();
        ObjectOutputStream oos = new ObjectOutputStream(bos);
        oos.writeObject(ex1);
        oos.flush();
        ByteArrayInputStream bis = new ByteArrayInputStream(bos.toByteArray());
        ObjectInputStream ois = new ObjectInputStream(bis);
        Exception ex2 = (Exception) ois.readObject();
        assertEquals("msg", ex2.getMessage());
        assertEquals("f", ex2.getCause().getMessage());
    }
}
