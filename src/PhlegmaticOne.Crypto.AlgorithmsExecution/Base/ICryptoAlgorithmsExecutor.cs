using PhlegmaticOne.Crypto.Core.Base;
using PhlegmaticOne.Crypto.Core.Models;

namespace PhlegmaticOne.Crypto.AlgorithmsExecution.Base;

public interface ICryptoAlgorithmsExecutor
{
    EncryptionResult Encrypt(string message, IEncryptionData encryptionData);
    DecryptionResult Decrypt(EncryptionResult encryptionResult);
}