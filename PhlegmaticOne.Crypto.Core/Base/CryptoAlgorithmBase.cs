using PhlegmaticOne.Crypto.Core.Models;

namespace PhlegmaticOne.Crypto.Core.Base;

public abstract class CryptoAlgorithmBase<T> : ICryptoAlgorithm<T> where T : IEncryptionData
{
    public abstract string Description { get; }

    public object Encrypt(string textToEncrypt, object encryptionData)
    {
        if (encryptionData is T genericEncryptionData)
        {
            return Encrypt(textToEncrypt, genericEncryptionData);
        }

        throw new ArgumentException("Encryption data doesn't fit to encryption algorithm", nameof(encryptionData));
    }

    public DecryptionResult Decrypt(object encryptionResult)
    {
        if (encryptionResult is EncryptionResult<T> genericEncryptionResult)
        {
            return Decrypt(genericEncryptionResult);
        }

        throw new ArgumentException("Encryption result doesn't fit to encryption algorithm", nameof(encryptionResult));
    }
    public abstract EncryptionResult<T> Encrypt(string textToEncrypt, T encryptionData);
    public abstract DecryptionResult Decrypt(EncryptionResult<T> encryptionResult);
}