using PhlegmaticOne.Crypto.Core.Alphabet;
using PhlegmaticOne.Crypto.Symmetric.Gamma;
using PhlegmaticOne.Crypto.Symmetric.Gamma.EncryptionData;
using PhlegmaticOne.Crypto.Symmetric.Gamma.KeyGenerators;

namespace PhlegmaticOne.Crypto.Tests;

public class GammaAlgorithmTests
{
    private readonly GammaAlgorithm _algorithm;
    private readonly GammaAlgorithmEncryptionData _algorithmData;
    public GammaAlgorithmTests()
    {
        var alphabetString = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя ";
        var letterToDigitConverter = LetterToDigitConverter.FromAlphabetString(alphabetString);
        var keyGenerator = new RandomKeyGenerator();

        _algorithmData = new GammaAlgorithmEncryptionData(letterToDigitConverter, keyGenerator, letterToDigitConverter.Length);
        _algorithm = new GammaAlgorithm();
    }
    [Theory]
    [InlineData("криптографический")]
    [InlineData("ъъъъъьъъьььььььъьъъьъьъъъъъььь")]
    [InlineData("я л ю б л ю п р о б е л ы")]
    [InlineData("кротов александр вячеславович")]
    [InlineData("ыалтыщпш гфтыпфцузпатфц уацзп фыдалтфыфаыафшщцша")]
    public void Gamma_Test(string encrypting)
    {
        var encrypted = _algorithm.Encrypt(encrypting, _algorithmData);
        var decrypted = _algorithm.Decrypt(encrypted);

        Assert.Equal(encrypting, decrypted.DecryptedText);
    }
}
