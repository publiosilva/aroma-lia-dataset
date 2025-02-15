import pytest

class TestComparisons:

    @pytest.mark.parametrize("fakeBus, program", [(FakeBus.create(), {
        0x0080: 0xDD,  # CP (IX+3)
        0x0081: 0xBE,
        0x0082: 0x03,
        0x0083: 0x00,
        0x0084: 0x00,
        0x08FB: 0x00,
        0x08FC: 0x00,
        0x08FD: 0x00,
        0x08FE: 0x00,
        0x08FF: 0x00,  # <- (IX)
        0x0900: 0x00,
        0x0901: 0x00,
        0x0902: 0x01,
        0x0903: 0x00,
        0x0904: 0x00,
        0x0905: 0x00,
        0x0906: 0x00,
    })])
    def test_unset_zero_flag_when_false_for_CPIXD1(self, fakeBus, program):
        when(fakeBus.read(any_short(), any_boolean())).then_answer(lambda invocation: program.get(invocation.get_argument(0)))
        cpu = Z80()
        cpu.A = 0x02
        cpu.IX = 0x08FF
        cpu.PC = 0x0080
        cpu.connect_to_bus(fakeBus)
        cpu.step()
        assert cpu.A == 0x02, "Explanation message"
        assert (cpu.F & Z80.Flags.N) == Z80.Flags.N, "Explanation message"
        assert (cpu.F & Z80.Flags.Z) != Z80.Flags.Z, "Explanation message"
        assert (cpu.F & Z80.Flags.S) != Z80.Flags.S, "Explanation message"
        assert (cpu.F & Z80.Flags.H) != Z80.Flags.H, "Explanation message"
        assert (cpu.F & Z80.Flags.P) != Z80.Flags.P, "Explanation message"
        assert (cpu.F & Z80.Flags.C) != Z80.Flags.C, "Explanation message"
        assert (cpu.F & Z80.Flags.U) != Z80.Flags.U, "Explanation message"
        assert (cpu.F & Z80.Flags.X) != Z80.Flags.X, "Explanation message"
