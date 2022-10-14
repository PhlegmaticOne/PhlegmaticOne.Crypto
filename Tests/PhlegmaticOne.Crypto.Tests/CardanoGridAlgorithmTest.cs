using PhlegmaticOne.Crypto.Core.Alphabet;
using PhlegmaticOne.Crypto.Symmetric.CardanoGrid;
using PhlegmaticOne.Crypto.Symmetric.CardanoGrid.EncryptionData;
using PhlegmaticOne.Crypto.Symmetric.CardanoGrid.Masks;

namespace PhlegmaticOne.Crypto.Tests;

public class CardanoGridAlgorithmTest
{
    [Theory]
    [InlineData("криптографический")]
    [InlineData("ъъъъъьъъьььььььъьъъьъьъъъъъььь")]
    [InlineData("я л ю б л ю п р о б е л ы")]
    [InlineData("кротов александр вячеславович")]
    [InlineData("ыалтыщпш гфтыпфцузпатфц уацзп фыдалтфыфаыафшщцша")]
    public void CardanoGrid_Test(string encrypting)
    {
        var alphabetString = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя ";
        var letterToDigitConverter = LetterToDigitConverter.FromAlphabetString(alphabetString);
        var maskGenerator = new RandomMaskGenerator();
        var algorithmData = new CardanoGridAlgorithmEncryptionData(maskGenerator, letterToDigitConverter);
        var algorithm = new CardanoGridAlgorithm();

        var encrypted = algorithm.Encrypt(encrypting, algorithmData);
        var decrypted = algorithm.Decrypt(encrypted);

        Assert.Equal(encrypting, decrypted.DecryptedText);
    }
}
