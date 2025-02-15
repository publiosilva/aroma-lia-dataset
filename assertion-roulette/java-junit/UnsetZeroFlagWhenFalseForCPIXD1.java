import static org.junit.jupiter.api.Assertions.*;
import org.junit.jupiter.api.Test;

public class ComparisonsShould {

    @Test
    public void unsetZeroFlagWhenFalseForCPIXD1() {
        IBus fakeBus = FakeBus.create();
        Map<Short, Byte> program = new HashMap<Short, Byte>() {{
            put((short) 0x0080, (byte) 0xDD); // CP (IX+3)
            put((short) 0x0081, (byte) 0xBE);
            put((short) 0x0082, (byte) 0x03);
            put((short) 0x0083, (byte) 0x00);
            put((short) 0x0084, (byte) 0x00);
            put((short) 0x08FB, (byte) 0x00);
            put((short) 0x08FC, (byte) 0x00);
            put((short) 0x08FD, (byte) 0x00);
            put((short) 0x08FE, (byte) 0x00);
            put((short) 0x08FF, (byte) 0x00); // <- (IX)
            put((short) 0x0900, (byte) 0x00);
            put((short) 0x0901, (byte) 0x00);
            put((short) 0x0902, (byte) 0x01);
            put((short) 0x0903, (byte) 0x00);
            put((short) 0x0904, (byte) 0x00);
            put((short) 0x0905, (byte) 0x00);
            put((short) 0x0906, (byte) 0x00);
        }};
        when(fakeBus.read(anyShort(), anyBoolean())).thenAnswer(invocation -> program.get(invocation.getArgument(0)));
        Z80 cpu = new Z80();
        cpu.A = 0x02;
        cpu.IX = 0x08FF;
        cpu.PC = 0x0080;
        cpu.connectToBus(fakeBus);
        cpu.step();
        assertEquals(0x02, cpu.A);
        assertTrue((cpu.F & Z80.Flags.N) == Z80.Flags.N);
        assertFalse((cpu.F & Z80.Flags.Z) == Z80.Flags.Z);
        assertFalse((cpu.F & Z80.Flags.S) == Z80.Flags.S);
        assertFalse((cpu.F & Z80.Flags.H) == Z80.Flags.H);
        assertFalse((cpu.F & Z80.Flags.P) == Z80.Flags.P);
        assertFalse((cpu.F & Z80.Flags.C) == Z80.Flags.C);
        assertFalse((cpu.F & Z80.Flags.U) == Z80.Flags.U);
        assertFalse((cpu.F & Z80.Flags.X) == Z80.Flags.X);
    }
}
