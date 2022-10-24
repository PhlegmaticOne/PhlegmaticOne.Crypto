namespace PhlegmaticOne.Crypto.Symmetric.Feistel.Core.Keys;

public interface IFeistelRoundKeysGenerator
{
    IEnumerable<FeistelKey> GenerateRoundKeys(int totalRounds, int keySizeInBits);
}