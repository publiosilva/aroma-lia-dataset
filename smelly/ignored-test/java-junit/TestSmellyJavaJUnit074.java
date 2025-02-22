import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.condition.Disabled;

import static org.junit.jupiter.api.Assertions.assertNull;

class Get {
    @Test
    @Disabled("")
    public void itReturnsNullWhenNotFound() {
        // Arrange
        // Act
        var result = _repo.get(-1);
        // Assert
        assertNull(result);
    }
}
