using PhlegmaticOne.Crypto.Core.Alphabet;
using PhlegmaticOne.Crypto.Symmetric.Gamma.EncryptionData;
using PhlegmaticOne.Crypto.Gamma;
using PhlegmaticOne.Crypto.Symmetric.Gamma.KeyGenerators;
using PhlegmaticOne.Crypto.DigitalCryptography.EncryptionData;
using PhlegmaticOne.Crypto.DigitalCryptography;

namespace PhlegmaticOne.Crypto.Tests;

public class GammaAlgorithmTests
{
    private readonly GammaAlgorithm _algorithm;
    private readonly GammaAlgorithmEncryptionData _algorithmData;
    public GammaAlgorithmTests()
    {
        var alphabetString = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя ";
        var alphabet = alphabetString.Select((x, i) => (x, i)).ToDictionary(x => x.x, x => x.i);
        var letterToDigitConverter = new LetterToDigitConverter(alphabet);
        var keyGenerator = new RandomKeyGenerator();

        _algorithmData = new GammaAlgorithmEncryptionData(letterToDigitConverter, keyGenerator, alphabet.Count);
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
