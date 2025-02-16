using Xunit;

public class ScriptTest
{
    [Fact]
    public void TestSerialization1()
    {
        string scr = "OP_ADD OP_IF OP_DUP OP_HASH160 0x68bf827a2fa3b31e53215e5dd19260d21fdf053e OP_EQUALVERIFY OP_CHECKSIG OP_ELSE OP_IF OP_DUP OP_ELSE OP_2ROT OP_ENDIF OP_HASH160 0x68bf827a2fa3b31e53215e5dd19260d21fdf053e OP_EQUAL OP_ENDIF OP_PUSHDATA1 0x4e 0x010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101";

        Script s = new Script(scr);
        Assert.Null(s.raw_script);
        Assert.Equal(s.script, scr);

        System.Console.WriteLine(s.ast);
        byte[] s_bytes = s.toBytes();
        string s_hex_str = bytesToStr(s_bytes);
        System.Console.WriteLine(s_hex_str);
        System.Console.WriteLine(s);

        Script s1 = Script.fromBytes(packVarStr(s_bytes))[0];
        Assert.Equal(s1.raw_script, s_bytes);
        System.Console.WriteLine(s1.ast);

        string raw_scr = "483045022100d60baf72dbaed8d15c3150e3309c9f7725fbdf91b0560330f3e2a0ccb606dfba02206422b1c73ce390766f0dc4e9143d0fbb400cc207e4a9fd9130e7f79e52bf81220121034ccd52d8f72cfdd680077a1a171458a1f7574ebaa196095390ae45e68adb3688";
        s = new Script(hexStringToByteArray(raw_scr));
        Assert.NotNull(s.raw_script);
        Assert.Null(s.script);

        System.Console.WriteLine(s);

        s_hex_str = bytesToStr(s.toBytes());
        Assert.Equal(s_hex_str, raw_scr);
        Assert.NotNull(s.script);
    }

    [Fact]
    public void TestSerialization2()
    {
        string scr = "OP_ADD OP_IF OP_DUP OP_HASH160 0x68bf827a2fa3b31e53215e5dd19260d21fdf053e OP_EQUALVERIFY OP_CHECKSIG OP_ELSE OP_IF OP_DUP OP_ELSE OP_2ROT OP_ENDIF OP_HASH160 0x68bf827a2fa3b31e53215e5dd19260d21fdf053e OP_EQUAL OP_ENDIF OP_PUSHDATA1 0x4e 0x010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101";

        Script s = new Script(scr);
        Assert.Null(s.raw_script);
        Assert.Equal(s.script, scr);

        System.Console.WriteLine(s.ast);
        byte[] s_bytes = s.toBytes();
        string s_hex_str = bytesToStr(s_bytes);
        System.Console.WriteLine(s_hex_str);
        System.Console.WriteLine(s);

        Script s1 = Script.fromBytes(packVarStr(s_bytes))[0];
        Assert.Equal(s1.raw_script, s_bytes);
        System.Console.WriteLine(s1.ast);

        string raw_scr = "483045022100d60baf72dbaed8d15c3150e3309c9f7725fbdf91b0560330f3e2a0ccb606dfba02206422b1c73ce390766f0dc4e9143d0fbb400cc207e4a9fd9130e7f79e52bf81220121034ccd52d8f72cfdd680077a1a171458a1f7574ebaa196095390ae45e68adb3688";
        s = new Script(hexStringToByteArray(raw_scr));
        Assert.NotNull(s.raw_script);
        Assert.Null(s.script);

        System.Console.WriteLine(s);

        s_hex_str = bytesToStr(s.toBytes());
        Assert.Equal(s_hex_str, raw_scr);

        s.parse();
        Assert.NotNull(s.script);
    }
}
