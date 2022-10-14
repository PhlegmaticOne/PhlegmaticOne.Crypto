using PhlegmaticOne.Crypto.Core.Alphabet;
using PhlegmaticOne.Crypto.DigitalCryptography.EncryptionData;
using PhlegmaticOne.Crypto.DigitalCryptography.LettersEncryption;
using PhlegmaticOne.Crypto.DigitalCryptography;
using PhlegmaticOne.Crypto.PolybiusSquare.Alphabet;
using PhlegmaticOne.Crypto.PolybiusSquare.EncryptionData;
using PhlegmaticOne.Crypto.PolybiusSquare.LettersEncryption;
using PhlegmaticOne.Crypto.PolybiusSquare;

namespace PhlegmaticOne.Crypto.Tests;

public class PolybiusSquareTests
{
    private readonly PolibiusSquareAlgorithm _algorithm;
    private readonly SquareAlphabet _squareAlphabet;
    public PolybiusSquareTests()
    {
        var alphabet = "абвгдежзийклмнопрстуфхцчшщъыьэюя ";

        _squareAlphabet = SquareAlphabet.FromAlphabet(alphabet);
        _algorithm = new PolibiusSquareAlgorithm();
    }

    [Theory]
    [InlineData("л о г у н о в а о л е ч к а")]
    [InlineData("защита информации")]
    [InlineData("вааааааап фы фы ыфщлаффытщав  ыываыщаштфщзт ываываы")]
    [InlineData("кротов александр вячеславович")]
    [InlineData("большоесловобезпробелов")]
    public void OneRowDowsPolicy_Enryption_Tests(string encrypting)
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
    public void ReadByRowsPolicy_Enryption_Tests(string encrypting)
    {
        var letterEncryptionPolicy = new ReadPolibiusEncryptedDigitCodesByRowsLetterEncryptionPolicy(_squareAlphabet);
        var algorithmData = new PolybiusSquareEncryptionData(letterEncryptionPolicy);

        var encrypted = _algorithm.Encrypt(encrypting, algorithmData);
        var decrypted = _algorithm.Decrypt(encrypted);

        Assert.Equal(encrypting, decrypted.DecryptedText);
    }
}
