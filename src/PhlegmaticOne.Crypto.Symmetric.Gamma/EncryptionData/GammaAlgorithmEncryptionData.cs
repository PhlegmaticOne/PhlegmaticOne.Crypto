using PhlegmaticOne.Crypto.Core.Alphabet;
using PhlegmaticOne.Crypto.Core.Base;
using PhlegmaticOne.Crypto.Symmetric.Gamma.KeyGenerators;

namespace PhlegmaticOne.Crypto.Symmetric.Gamma.EncryptionData;

public class GammaAlgorithmEncryptionData : IEncryptionData
{
    private readonly IKeyGenerator _keyGenerator;

    public ILetterToDigitConverter Alphabet { get; }
    public IEnumerable<int> Key { get; private set; }
    public int Mod { get; }
    public GammaAlgorithmEncryptionData(ILetterToDigitConverter alphabet, IKeyGenerator keyGenerator, int mod)
    {
        _keyGenerator = keyGenerator;
        Key = Enumerable.Empty<int>();
        Alphabet = alphabet;
        Mod = mod;
    }
    public IEnumerable<int> GenerateKey(int keyLength)
    {
        var key = _keyGenerator.GenerateKey(keyLength, Alphabet.Length);
        Key = key;
        return key;
    }
}