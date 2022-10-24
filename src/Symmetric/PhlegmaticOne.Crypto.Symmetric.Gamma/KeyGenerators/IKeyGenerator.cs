namespace PhlegmaticOne.Crypto.Symmetric.Gamma.KeyGenerators;

public interface IKeyGenerator
{
    IEnumerable<int> GenerateKey(int keyLength, int alphabetLength);
}
