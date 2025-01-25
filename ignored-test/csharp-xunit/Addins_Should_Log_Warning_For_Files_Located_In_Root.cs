using Xunit;

namespace Cake.NuGet.Tests(net6.0)
{
    public class Addins
    {
        [Fact(Skip = "")]
        public void Should_Log_Warning_For_Files_Located_In_Root()
        {
            {
                // Given
                var fixture = new NuGetAddinContentResolverFixture(framework, runtime);
                fixture.CreateCLRAssembly("/Working/file.dll");
                fixture.CreateCLRAssembly("/Working/file2.dll");
                fixture.CreateCLRAssembly("/Working/file3.dll");
                // When
                fixture.GetFiles();
                // Then
                var entries = fixture.Log.Entries.Where(x => x.Level == LogLevel.Warning && x.Message.Equals($"Could not find any assemblies compatible with {framework} in NuGet package {fixture.Package.Package}. " + "Falling back to using root folder of NuGet package.")).ToList();
                Assert.Single(entries);
            }
        }
    }
}
