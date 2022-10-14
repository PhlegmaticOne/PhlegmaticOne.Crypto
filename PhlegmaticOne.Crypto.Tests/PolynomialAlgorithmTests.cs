using PhlegmaticOne.Crypto.Core.Alphabet;
using PhlegmaticOne.Crypto.Symmetric.Polynomial;
using PhlegmaticOne.Crypto.Symmetric.Polynomial.EncryptionData;

namespace PhlegmaticOne.Crypto.Tests;

public class PolynomialAlgorithmTests
{
    private readonly PolynomialAlgorithm _algorithm;
    private readonly PolynomialAlgorithmEncryptionData _algorithmData;
    public PolynomialAlgorithmTests()
    {
        var alphabetString = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя ";
        var letterToDigitConverter = LetterToDigitConverter.FromAlphabetString(alphabetString);

        var polynomialFunc = (int x) => x * x * x + 2 * x * x + 3 * x + 4;
        var mod = 911;
        var separatingChar = ' ';
        _algorithmData = new PolynomialAlgorithmEncryptionData(letterToDigitConverter, polynomialFunc, mod, separatingChar);
        _algorithm= new PolynomialAlgorithm();
    }
    [Theory]
    [InlineData("криптографический")]
    [InlineData("ъъъъъьъъьььььььъьъъьъьъъъъъььь")]
    [InlineData("я л ю б л ю п р о б е л ы")]
    [InlineData("кротов александр вячеславович")]
    [InlineData("ыалтыщпш гфтыпфцузпатфц уацзп фыдалтфыфаыафшщцша")]
    public void Polynomial_Test(string encrypting)
    {
        var encrypted = _algorithm.Encrypt(encrypting, _algorithmData);
        var decrypted = _algorithm.Decrypt(encrypted);

        Assert.Equal(encrypting, decrypted.DecryptedText);
    }
}
