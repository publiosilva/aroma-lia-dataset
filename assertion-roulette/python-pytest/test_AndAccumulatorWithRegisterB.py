import pytest
from unittest.mock import MagicMock

class TestEightBitLogicGroup:
    def test_and_accumulator_with_register_b(self):
        fake_bus = MagicMock()
        program = {
            0x0080: 0xA0,  # AND B
            0x0081: 0x00,
            0x0082: 0x00,
            0x0083: 0x00,
            0x0084: 0x00,
        }
        fake_bus.read.side_effect = lambda addr, ro: program[addr]
        cpu = Z80()
        cpu.A = 0xC3
        cpu.B = 0x7B
        cpu.PC = 0x0080
        cpu.connect_to_bus(fake_bus)
        cpu.step()
        assert cpu.A == 0x43  # 01100011 & 01111011 = 01000011
        assert not (cpu.F & Z80.Flags.N) == Z80.Flags.N
        assert not (cpu.F & Z80.Flags.Z) == Z80.Flags.Z
        assert not (cpu.F & Z80.Flags.S) == Z80.Flags.S
        assert (cpu.F & Z80.Flags.H) == Z80.Flags.H
        assert not (cpu.F & Z80.Flags.P) == Z80.Flags.P
        assert not (cpu.F & Z80.Flags.C) == Z80.Flags.C
        assert not (cpu.F & Z80.Flags.U) == Z80.Flags.U
        assert not (cpu.F & Z80.Flags.X) == Z80.Flags.X
