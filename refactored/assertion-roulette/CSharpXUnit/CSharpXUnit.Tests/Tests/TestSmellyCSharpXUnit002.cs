using Xunit;

namespace DefaultNamespace
{
    public class EightBitLogicGroupShould
    {
        [Fact]
        public void AndAccumulatorWithRegisterB1()
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
            }
        }
        
        [Fact]
        public void AndAccumulatorWithRegisterB2()
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
                Assert.False((cpu.F & Z80.Flags.N) == Z80.Flags.N);
            }
        }
        
        [Fact]
        public void AndAccumulatorWithRegisterB3()
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
                Assert.False((cpu.F & Z80.Flags.Z) == Z80.Flags.Z);
            }
        }
        
        [Fact]
        public void AndAccumulatorWithRegisterB3()
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
                Assert.False((cpu.F & Z80.Flags.S) == Z80.Flags.S);
            }
        }
        
        [Fact]
        public void AndAccumulatorWithRegisterB5()
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
                Assert.True((cpu.F & Z80.Flags.H) == Z80.Flags.H);
            }
        }
        
        [Fact]
        public void AndAccumulatorWithRegisterB6()
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
                Assert.False((cpu.F & Z80.Flags.P) == Z80.Flags.P);
            }
        }
        
        [Fact]
        public void AndAccumulatorWithRegisterB7()
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
                Assert.False((cpu.F & Z80.Flags.C) == Z80.Flags.C);
            }
        }
        
        [Fact]
        public void AndAccumulatorWithRegisterB8()
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
                Assert.False((cpu.F & Z80.Flags.U) == Z80.Flags.U);
            }
        }
        
        [Fact]
        public void AndAccumulatorWithRegisterB9()
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
                Assert.False((cpu.F & Z80.Flags.X) == Z80.Flags.X);
            }
        }

    }
}
