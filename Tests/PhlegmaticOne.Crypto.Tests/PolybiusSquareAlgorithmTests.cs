using PhlegmaticOne.Crypto.ClassicCrypto.PolybiusSquare;
using PhlegmaticOne.Crypto.ClassicCrypto.PolybiusSquare.Alphabet;
using PhlegmaticOne.Crypto.ClassicCrypto.PolybiusSquare.EncryptionData;
using PhlegmaticOne.Crypto.ClassicCrypto.PolybiusSquare.LettersEncryption;
using PhlegmaticOne.Crypto.Core.Alphabet;

namespace PhlegmaticOne.Crypto.Tests;

public class PolybiusSquareAlgorithmTests
{
    private readonly PolybiusSquareAlgorithm _algorithm;
    private readonly SquareAlphabet _squareAlphabet;
    public PolybiusSquareAlgorithmTests()
    {
        var alphabet = "абвгдежзийклмнопрстуфхцчшщъыьэюя ";

        _squareAlphabet = SquareAlphabet.FromAlphabet(alphabet);
        _algorithm = new PolybiusSquareAlgorithm();
    }

    [Theory]
    [InlineData("л о г у н о в а о л е ч к а")]
    [InlineData("защита информации")]
    [InlineData("вааааааап фы фы ыфщлаффытщав  ыываыщаштфщзт ываываы")]
    [InlineData("кротов александр вячеславович")]
    [InlineData("большоесловобезпробелов")]
    public void OneRowRowsPolicy_Encryption_Tests(string encrypting)
    {
        var letterEncryptionPolicy = new OneRowDownEncryptionPolicy(_squareAlphabet);
        var algorithmData = new PolybiusSquareEncryptionData(letterEncryptionPolicy);

        var encrypted = _algorithm.Encrypt(encrypting, algorithmData);
        var decrypted = _algorithm.Decrypt(encrypted);

        Assert.Equal(encrypting, decrypted.DecryptedText);
    }

    [Theory]
    [InlineData("л о г у н о в а о л е ч к а")]
    [InlineData("защита информации")]
    [InlineData("вааааааап фы фы ыфщлаффытщав  ыываыщаштфщзт ываываы")]
    [InlineData("кротов александр вячеславович")]
    [InlineData("большоесловобезпробелов")]
    public void ReadByRowsPolicy_Encryption_Tests(string encrypting)
    {
        var letterEncryptionPolicy = new ReadPolybiusEncryptedDigitCodesByRowsLetterEncryptionPolicy(_squareAlphabet);
        var algorithmData = new PolybiusSquareEncryptionData(letterEncryptionPolicy);

        var encrypted = _algorithm.Encrypt(encrypting, algorithmData);
        var decrypted = _algorithm.Decrypt(encrypted);

        Assert.Equal(encrypting, decrypted.DecryptedText);
    }
}
