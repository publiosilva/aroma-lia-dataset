using Xunit;
using Two1.Bitcoin.Script;
using Two1.Bitcoin.Utils;

public class ScriptTests
{
    [Fact]
    public void TestSerialization()
    {
        var scr = "OP_ADD OP_IF OP_DUP OP_HASH160 0x68bf827a2fa3b31e53215e5dd19260d21fdf053e OP_EQUALVERIFY OP_CHECKSIG OP_ELSE OP_IF OP_DUP OP_ELSE OP_2ROT OP_ENDIF OP_HASH160 0x68bf827a2fa3b31e53215e5dd19260d21fdf053e OP_EQUAL OP_ENDIF OP_PUSHDATA1 0x4e 0x010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101";

        var s = new Script(scr);
        Assert.Null(s.RawScript);
        Assert.Equal(scr, s.Script);

        System.Diagnostics.Debug.WriteLine(s.Ast);
        var sBytes = s.ToBytes();
        var sHexString = BytesToStr(sBytes);
        System.Diagnostics.Debug.WriteLine(sHexString);
        System.Diagnostics.Debug.WriteLine(s);

        var s1 = Script.FromBytes(PackVarStr(sBytes))[0];
        Assert.Equal(sBytes, s1.RawScript);
        System.Diagnostics.Debug.WriteLine(s1.Ast);

        var rawScr = "483045022100d60baf72dbaed8d15c3150e3309c9f7725fbdf91b0560330f3e2a0ccb606dfba02206422b1c73ce390766f0dc4e9143d0fbb400cc207e4a9fd9130e7f79e52bf81220121034ccd52d8f72cfdd680077a1a171458a1f7574ebaa196095390ae45e68adb3688";
        s = new Script(HexToBytes(rawScr));
        Assert.NotNull(s.RawScript);
        Assert.Null(s.Script);

        System.Diagnostics.Debug.WriteLine(s);

        sHexString = BytesToStr(s.ToBytes());
        Assert.Equal(rawScr, sHexString);
        Assert.NotNull(s.Script);

        s.Parse();
        Assert.NotNull(s.Script);
    }
}
