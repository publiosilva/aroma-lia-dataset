import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.Mockito.*;

class EightBitLogicGroupShould {

    @Test
    void andAccumulatorWithRegisterB() {
        IBus fakeBus = mock(IBus.class);
        Map<Short, Byte> program = new HashMap<>();
        program.put((short) 0x0080, (byte) 0xA0); // AND B
        program.put((short) 0x0081, (byte) 0x00);
        program.put((short) 0x0082, (byte) 0x00);
        program.put((short) 0x0083, (byte) 0x00);
        program.put((short) 0x0084, (byte) 0x00);
        
        when(fakeBus.read(anyShort(), anyBoolean())).thenAnswer(invocation -> program.get(invocation.getArgument(0)));

        Z80 cpu = new Z80();
        cpu.A = 0xC3;
        cpu.B = 0x7B;
        cpu.PC = 0x0080;
        cpu.connectToBus(fakeBus);
        cpu.step();
        
        assertEquals("Explanation message", 0x43, cpu.A); // 01100011 & 01111011 = 01000011
        assertFalse("Explanation message", (cpu.F & Z80.Flags.N) == Z80.Flags.N);
        assertFalse("Explanation message", (cpu.F & Z80.Flags.Z) == Z80.Flags.Z);
        assertFalse("Explanation message", (cpu.F & Z80.Flags.S) == Z80.Flags.S);
        assertTrue("Explanation message", (cpu.F & Z80.Flags.H) == Z80.Flags.H);
        assertFalse("Explanation message", (cpu.F & Z80.Flags.P) == Z80.Flags.P);
        assertFalse("Explanation message", (cpu.F & Z80.Flags.C) == Z80.Flags.C);
        assertFalse("Explanation message", (cpu.F & Z80.Flags.U) == Z80.Flags.U);
        assertFalse("Explanation message", (cpu.F & Z80.Flags.X) == Z80.Flags.X);
    }
}
