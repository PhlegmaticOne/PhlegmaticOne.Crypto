namespace PhlegmaticOne.Crypto.Core.Base;

public interface ICryptoAlgorithm<T> where T : IEncryptionData
{
    EncryptionResult<T> Encrypt(string textToEncrypt, T encryptionData);
    DecryptionResult Decrypt(EncryptionResult<T> encryptionResult);
}
public interface ICryptoAlgorithm { }