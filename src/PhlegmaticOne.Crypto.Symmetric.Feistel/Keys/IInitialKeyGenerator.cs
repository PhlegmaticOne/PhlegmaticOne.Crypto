namespace PhlegmaticOne.Crypto.Symmetric.Feistel.Keys;

public interface IInitialKeyGenerator
{
    FeistelKey GenerateKey(int sizeInBits);
}
