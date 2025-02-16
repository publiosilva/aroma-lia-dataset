using Xunit;

public class ComparisonsShould {

    [Fact]
    public void unsetZeroFlagWhenFalseForCPIXD1() {
        IBus fakeBus = FakeBus.Create();
        var program = new Dictionary<short, byte> {
            { 0x0080, 0xDD }, // CP (IX+3)
            { 0x0081, 0xBE },
            { 0x0082, 0x03 },
            { 0x0083, 0x00 },
            { 0x0084, 0x00 },
            { 0x08FB, 0x00 },
            { 0x08FC, 0x00 },
            { 0x08FD, 0x00 },
            { 0x08FE, 0x00 },
            { 0x08FF, 0x00 }, // <- (IX)
            { 0x0900, 0x00 },
            { 0x0901, 0x00 },
            { 0x0902, 0x01 },
            { 0x0903, 0x00 },
            { 0x0904, 0x00 },
            { 0x0905, 0x00 },
            { 0x0906, 0x00 }
        };
        
        A.CallTo(() => fakeBus.Read(A<short>.Ignored, A<bool>.Ignored)).ReturnsLazily(invocation => program[(short)invocation.Arguments[0]]);
        Z80 cpu = new Z80();
        cpu.A = 0x02;
        cpu.IX = 0x08FF;
        cpu.PC = 0x0080;
        cpu.ConnectToBus(fakeBus);
        cpu.Step();

        Assert.Equal(0x02, cpu.A, "Explanation message");
        Assert.True((cpu.F & Z80.Flags.N) == Z80.Flags.N, "Explanation message");
        Assert.False((cpu.F & Z80.Flags.Z) == Z80.Flags.Z, "Explanation message");
        Assert.False((cpu.F & Z80.Flags.S) == Z80.Flags.S, "Explanation message");
        Assert.False((cpu.F & Z80.Flags.H) == Z80.Flags.H, "Explanation message");
        Assert.False((cpu.F & Z80.Flags.P) == Z80.Flags.P, "Explanation message");
        Assert.False((cpu.F & Z80.Flags.C) == Z80.Flags.C, "Explanation message");
        Assert.False((cpu.F & Z80.Flags.U) == Z80.Flags.U, "Explanation message");
        Assert.False((cpu.F & Z80.Flags.X) == Z80.Flags.X, "Explanation message");
    }
}
