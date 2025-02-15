using Xunit;

namespace DefaultNamespace
{
    public class ComparisonsShould
    {
        [Fact]
        public void UnsetZeroFlagWhenFalseForCPIXD1()
        {
            {
                var fakeBus = A.Fake<IBus>();
                var program = new Dictionary<ushort, byte>
                {
                    // Program Code
                    {
                        0x0080,
                        0xDD
                    }, // CP (IX+3)
                    {
                        0x0081,
                        0xBE
                    },
                    {
                        0x0082,
                        0x03
                    },
                    {
                        0x0083,
                        0x00
                    },
                    {
                        0x0084,
                        0x00
                    },
                    // Data
                    {
                        0x08FB,
                        0x00
                    },
                    {
                        0x08FC,
                        0x00
                    },
                    {
                        0x08FD,
                        0x00
                    },
                    {
                        0x08FE,
                        0x00
                    },
                    {
                        0x08FF,
                        0x00
                    }, // <- (IX)
                    {
                        0x0900,
                        0x00
                    },
                    {
                        0x0901,
                        0x00
                    },
                    {
                        0x0902,
                        0x01
                    },
                    {
                        0x0903,
                        0x00
                    },
                    {
                        0x0904,
                        0x00
                    },
                    {
                        0x0905,
                        0x00
                    },
                    {
                        0x0906,
                        0x00
                    },
                };
                A.CallTo(() => fakeBus.Read(A<ushort>._, A<bool>._)).ReturnsLazily((ushort addr, bool ro) => program[addr]);
                var cpu = new Z80()
                {
                    A = 0x02,
                    IX = 0x08FF,
                    PC = 0x0080
                };
                cpu.ConnectToBus(fakeBus);
                cpu.Step();
                Assert.Equal(0x02, cpu.A);
                Assert.True((cpu.F & Z80.Flags.N) == Z80.Flags.N);
                Assert.False((cpu.F & Z80.Flags.Z) == Z80.Flags.Z);
                Assert.False((cpu.F & Z80.Flags.S) == Z80.Flags.S);
                Assert.False((cpu.F & Z80.Flags.H) == Z80.Flags.H);
                Assert.False((cpu.F & Z80.Flags.P) == Z80.Flags.P);
                Assert.False((cpu.F & Z80.Flags.C) == Z80.Flags.C);
                Assert.False((cpu.F & Z80.Flags.U) == Z80.Flags.U);
                Assert.False((cpu.F & Z80.Flags.X) == Z80.Flags.X);
            }
        }
    }
}
