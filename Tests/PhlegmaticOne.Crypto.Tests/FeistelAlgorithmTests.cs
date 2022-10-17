using PhlegmaticOne.Crypto.Symmetric.Feistel;
using PhlegmaticOne.Crypto.Symmetric.Feistel.EncryptionData;
using PhlegmaticOne.Crypto.Symmetric.Feistel.Keys;
using PhlegmaticOne.Crypto.Symmetric.Feistel.Functions;

namespace PhlegmaticOne.Crypto.Tests;

public class FeistelAlgorithmTests
{
    private readonly SymmetricFeistelAlgorithm _algorithm;
    private readonly FeistelAlgorithmData _algorithmData;
    public FeistelAlgorithmTests()
    {
        var keyGenerator = new RandomInitialKeyGenerator();
        var roundKeyGenerator = new ShiftRoundKeyGenerator();
        var function = new OrFeistelFunction();
        var postFunction = new PBoxFunction();
        _algorithmData = new FeistelAlgorithmData(keyGenerator, 
            roundKeyGenerator, function, 
            new List<IPostFeistelFunction> { postFunction },
            32, 64);
        _algorithm = new SymmetricFeistelAlgorithm();
    }

    [Theory]
    [InlineData("стар")]
    [InlineData("кротов")]
    [InlineData("я л ю б л ю п р о б е л ы")]
    [InlineData("кротов александр вячеславович")]
    [InlineData("ыалтыщпш гфтыпфцузпатфц уацзп фыдалтфыфаыафшщцша")]
    public void Feistel_Tests(string toEncrypt)
    {
        var encrypted = _algorithm.Encrypt(toEncrypt, _algorithmData);
        var decrypted = _algorithm.Decrypt(encrypted);

        Assert.Equal(toEncrypt, decrypted.DecryptedText);
    }
}
