using Moq;
using Xunit;

public class EightBitLogicGroupShould {

    [Fact]
    public void AndAccumulatorWithRegisterB() {
        var fakeBus = new Mock<IBus>();
        var program = new Dictionary<short, byte> {
            { 0x0080, 0xA0 }, // AND B
            { 0x0081, 0x00 },
            { 0x0082, 0x00 },
            { 0x0083, 0x00 },
            { 0x0084, 0x00 }
        };
        
        fakeBus.Setup(bus => bus.Read(It.IsAny<short>(), It.IsAny<bool>()))
                .Returns((short address, bool something) => program[address]);

        var cpu = new Z80();
        cpu.A = 0xC3;
        cpu.B = 0x7B;
        cpu.PC = 0x0080;
        cpu.ConnectToBus(fakeBus.Object);
        cpu.Step();
        
        Assert.Equal(0x43, cpu.A, "Explanation message"); // 01100011 & 01111011 = 01000011
        Assert.False((cpu.F & Z80.Flags.N) == Z80.Flags.N, "Explanation message");
        Assert.False((cpu.F & Z80.Flags.Z) == Z80.Flags.Z, "Explanation message");
        Assert.False((cpu.F & Z80.Flags.S) == Z80.Flags.S, "Explanation message");
        Assert.True((cpu.F & Z80.Flags.H) == Z80.Flags.H, "Explanation message");
        Assert.False((cpu.F & Z80.Flags.P) == Z80.Flags.P, "Explanation message");
        Assert.False((cpu.F & Z80.Flags.C) == Z80.Flags.C, "Explanation message");
        Assert.False((cpu.F & Z80.Flags.U) == Z80.Flags.U, "Explanation message");
        Assert.False((cpu.F & Z80.Flags.X) == Z80.Flags.X, "Explanation message");
    }
}
