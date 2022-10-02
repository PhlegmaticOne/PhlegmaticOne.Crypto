namespace PhlegmaticOne.Crypto.Symmetric.Gamma.KeyGenerators;

public class RandomKeyGenerator : IKeyGenerator
{
    public IEnumerable<int> GenerateKey(int keyLength, int alphabetLength) =>
         Enumerable
        .Range(0, keyLength)
        .Select(x => Random.Shared.Next(0, alphabetLength))
        .ToList();
}