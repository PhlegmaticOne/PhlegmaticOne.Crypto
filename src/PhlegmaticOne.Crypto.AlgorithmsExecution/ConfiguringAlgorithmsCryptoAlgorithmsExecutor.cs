using PhlegmaticOne.Crypto.AlgorithmsExecution.Base;
using PhlegmaticOne.Crypto.Core.Base;
using PhlegmaticOne.Crypto.Core.Models;

namespace PhlegmaticOne.Crypto.AlgorithmsExecution;

public class ConfiguringAlgorithmsCryptoAlgorithmsExecutor : ICryptoAlgorithmsExecutor
{
    private readonly IEnumerable<ICryptoAlgorithm> _cryptoAlgorithms;

    public ConfiguringAlgorithmsCryptoAlgorithmsExecutor(IEnumerable<ICryptoAlgorithm> cryptoAlgorithms)
    {
        _cryptoAlgorithms = cryptoAlgorithms;
    }
    public EncryptionResult Encrypt(string message, IEncryptionData encryptionData)
    {
        var encryptionDataType = encryptionData.GetType();
        var algorithm = _cryptoAlgorithms.First(x => x.GetType()
            .GetInterfaces()
            .First()
            .GetGenericArguments()
            .First() == encryptionDataType);
        return algorithm.Encrypt(message, encryptionData);
    }

    public DecryptionResult Decrypt(EncryptionResult encryptionResult)
    {
        var algorithmType = encryptionResult.AlgorithmUsedDescription.Type;
        var algorithm = _cryptoAlgorithms
            .First(x => x.GetType() == algorithmType);
        return algorithm.Decrypt(encryptionResult);
    }
}