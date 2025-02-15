using Xunit;

namespace DefaultNamespace
{
    public class EightBitLogicGroupShould
    {
        [Fact]
        public void AndAccumulatorWithRegisterB()
        {
            {
                var fakeBus = A.Fake<IBus>();
                var program = new Dictionary<ushort, byte>
                {
                    // Program Code
                    {
                        0x0080,
                        0xA0
                    }, // AND B
                    {
                        0x0081,
                        0x00
                    },
                    {
                        0x0082,
                        0x00
                    },
                    {
                        0x0083,
                        0x00
                    },
                    {
                        0x0084,
                        0x00
                    },
                };
                A.CallTo(() => fakeBus.Read(A<ushort>._, A<bool>._)).ReturnsLazily((ushort addr, bool ro) => program[addr]);
                var cpu = new Z80()
                {
                    A = 0xC3,
                    B = 0x7B,
                    PC = 0x0080
                };
                cpu.ConnectToBus(fakeBus);
                cpu.Step();
                Assert.Equal(0x43, cpu.A); // 011000011 & 01111011 = 01000011
                Assert.False((cpu.F & Z80.Flags.N) == Z80.Flags.N);
                Assert.False((cpu.F & Z80.Flags.Z) == Z80.Flags.Z);
                Assert.False((cpu.F & Z80.Flags.S) == Z80.Flags.S);
                Assert.True((cpu.F & Z80.Flags.H) == Z80.Flags.H);
                Assert.False((cpu.F & Z80.Flags.P) == Z80.Flags.P);
                Assert.False((cpu.F & Z80.Flags.C) == Z80.Flags.C);
                Assert.False((cpu.F & Z80.Flags.U) == Z80.Flags.U);
                Assert.False((cpu.F & Z80.Flags.X) == Z80.Flags.X);
            }
        }
    }
}
