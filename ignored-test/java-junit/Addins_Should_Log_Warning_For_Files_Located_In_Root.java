import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.Disabled;
import static org.junit.jupiter.api.Assertions.assertEquals;

import java.util.List;
import java.util.stream.Collectors;

public class Addins {

    @Test
    @Disabled("")
    public void shouldLogWarningForFilesLocatedInRoot() {
        // Given
        NuGetAddinContentResolverFixture fixture = new NuGetAddinContentResolverFixture(framework, runtime);
        fixture.createCLRAssembly("/Working/file.dll");
        fixture.createCLRAssembly("/Working/file2.dll");
        fixture.createCLRAssembly("/Working/file3.dll");

        // When
        fixture.getFiles();

        // Then
        List<LogEntry> entries = fixture.getLog().getEntries().stream()
            .filter(x -> x.getLevel() == LogLevel.WARNING &&
                         x.getMessage().equals("Could not find any assemblies compatible with " + framework + 
                         " in NuGet package " + fixture.getPackage().getPackage() + 
                         ". Falling back to using root folder of NuGet package."))
            .collect(Collectors.toList());

        assertEquals(1, entries.size());
    }
}
