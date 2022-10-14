using PhlegmaticOne.Crypto.Core.Alphabet;
using PhlegmaticOne.Crypto.DigitalCryptography;
using PhlegmaticOne.Crypto.DigitalCryptography.EncryptionData;
using PhlegmaticOne.Crypto.DigitalCryptography.LettersEncryption;

namespace PhlegmaticOne.Crypto.Tests;

public class DigitalCryptographyTests
{
    private readonly DigitalCryptographyAlgorithm _digitalCryptographyAlgorithm;
    private readonly DigitalAlgorithmData _digitalAlgorithmData;
    public DigitalCryptographyTests()
    {
        var alphabet = new Dictionary<char, int>
        {
            { 'а', 1 }, { 'б', 2 }, { 'в', 3 }, { 'г', 4 }, { 'д', 5 }, { 'е', 6 }, { 'ж', 7 }, { 'з', 8 },
            { 'и', 10 }, { 'й', 20 }, { 'к', 30 }, { 'л', 40 }, { 'м', 50 }, { 'н', 60 }, { 'о', 70 }, { 'п', 80 },
            { 'р', 100 }, { 'с', 200 }, { 'т', 300 }, { 'у', 400 }, { 'ф', 500 }, { 'х', 600 }, { 'ц', 700 }, { 'ч', 800 },
            { 'ш', 1000 }, { 'щ', 2000 }, { 'ъ', 3000 }, { 'ы', 4000 }, { 'ь', 5000 }, { 'э', 6000 }, { 'ю', 7000 }, { 'я', 8000 },
        };

        char separateValue = '.';
        var letterDigitConverter = new LetterToDigitConverter(alphabet);
        var letterEncryptionPolicy = new SplitToMaxSymmetryWithTwoSizeEncryptionPolicy(letterDigitConverter);

        _digitalAlgorithmData = new DigitalAlgorithmData(letterEncryptionPolicy, separateValue);
        _digitalCryptographyAlgorithm = new DigitalCryptographyAlgorithm();
    }

    [Theory]
    [InlineData("криптографический")]
    [InlineData("ъъъъъьъъьььььььъьъъьъьъъъъъььь")]
    [InlineData("я л ю б л ю п р о б е л ы")]
    [InlineData("кротов александр вячеславович")]
    [InlineData("ыалтыщпш гфтыпфцузпатфц уацзп фыдалтфыфаыафшщцша")]
    public void Enryption_Tests(string encrypting)
    {
        var encrypted = _digitalCryptographyAlgorithm.Encrypt(encrypting, _digitalAlgorithmData);
        var decrypted = _digitalCryptographyAlgorithm.Decrypt(encrypted);

        Assert.Equal(encrypting, decrypted.DecryptedText);
    }
}
