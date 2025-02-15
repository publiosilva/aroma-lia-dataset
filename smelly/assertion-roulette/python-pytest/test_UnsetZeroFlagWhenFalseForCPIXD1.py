import pytest
from unittest import mock

class TestComparisons:
    def test_unset_zero_flag_when_false_for_cp_ixd1(self):
        fake_bus = mock.Mock()
        program = {
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
        }
        fake_bus.Read.side_effect = lambda addr, ro: program[addr]
        cpu = Z80()
        cpu.A = 0x02
        cpu.IX = 0x08FF
        cpu.PC = 0x0080
        cpu.connect_to_bus(fake_bus)
        cpu.step()
        assert cpu.A == 0x02
        assert (cpu.F & Z80.Flags.N) == Z80.Flags.N
        assert (cpu.F & Z80.Flags.Z) != Z80.Flags.Z
        assert (cpu.F & Z80.Flags.S) != Z80.Flags.S
        assert (cpu.F & Z80.Flags.H) != Z80.Flags.H
        assert (cpu.F & Z80.Flags.P) != Z80.Flags.P
        assert (cpu.F & Z80.Flags.C) != Z80.Flags.C
        assert (cpu.F & Z80.Flags.U) != Z80.Flags.U
        assert (cpu.F & Z80.Flags.X) != Z80.Flags.X
