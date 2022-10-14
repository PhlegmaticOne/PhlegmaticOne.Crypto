using PhlegmaticOne.Crypto.Core.Models;

namespace PhlegmaticOne.Crypto.Core.Base;

public interface ICryptoAlgorithm<T> : ICryptoAlgorithm where T : IEncryptionData
{
    EncryptionResult<T> Encrypt(string textToEncrypt, T encryptionData);
    DecryptionResult Decrypt(EncryptionResult<T> encryptionResult);
}

public interface ICryptoAlgorithm
{
    string Description { get; }
    EncryptionResult Encrypt(string textToEncrypt, IEncryptionData encryptionData);
    DecryptionResult Decrypt(EncryptionResult encryptionData);
}