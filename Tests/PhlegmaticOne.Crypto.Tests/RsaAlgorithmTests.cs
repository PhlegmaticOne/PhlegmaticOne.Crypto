using PhlegmaticOne.Crypto.Asymmetric.RSA;
using PhlegmaticOne.Crypto.Asymmetric.RSA.EncryptionData;
using PhlegmaticOne.Crypto.Core.Alphabet;

namespace PhlegmaticOne.Crypto.Tests;

public class RsaAlgorithmTests
{
    private readonly RsaAlgorithm _algorithm;
    private readonly RsaEncryptionData _algorithmData;
    public RsaAlgorithmTests()
    {
        var separatingChar = ' ';
        var primeNumbersLimitation = 500;
        var alphabetString = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя ";
        var alphabet = LetterToDigitConverter.FromAlphabetString(alphabetString);

        _algorithmData = new RsaEncryptionData(alphabet, primeNumbersLimitation, separatingChar);
        _algorithm = new RsaAlgorithm();
    }
    [Theory]
    [InlineData("криптографический")]
    [InlineData("ъъъъъьъъьььььььъьъъьъьъъъъъььь")]
    [InlineData("я л ю б л ю п р о б е л ы")]
    [InlineData("кротов александр вячеславович")]
    [InlineData("ыалтыщпш гфтыпфцузпатфц уацзп фыдалтфыфаыафшщцша")]
    public void Rsa_Test(string encrypting)
    {
        var encrypted = _algorithm.Encrypt(encrypting, _algorithmData);
        var decrypted = _algorithm.Decrypt(encrypted);

        Assert.Equal(encrypting, decrypted.DecryptedText);
    }
}