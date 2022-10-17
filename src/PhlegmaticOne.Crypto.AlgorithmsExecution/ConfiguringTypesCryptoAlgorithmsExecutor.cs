using PhlegmaticOne.Crypto.AlgorithmsExecution.Base;
using PhlegmaticOne.Crypto.Core.Base;
using PhlegmaticOne.Crypto.Core.Models;

namespace PhlegmaticOne.Crypto.AlgorithmsExecution;

public class ConfiguringTypesCryptoAlgorithmsExecutor : ICryptoAlgorithmsExecutor
{
    private readonly List<Type> _cryptoAlgorithmsTypes;

    public ConfiguringTypesCryptoAlgorithmsExecutor(IEnumerable<Type> cryptoAlgorithmsTypes)
    {
        _cryptoAlgorithmsTypes = cryptoAlgorithmsTypes.ToList();
    }
    public EncryptionResult Encrypt(string message, IEncryptionData encryptionData)
    {
        var encryptionDataType = encryptionData.GetType();
        var algorithmType = _cryptoAlgorithmsTypes.First(x => x.GetInterfaces()
            .First()
            .GetGenericArguments()
            .First() == encryptionDataType);
        var algorithm = CreateAlgorithm(algorithmType);
        return algorithm.Encrypt(message, encryptionData);
    }

    public DecryptionResult Decrypt(EncryptionResult encryptionResult)
    {
        var algorithmType = encryptionResult.AlgorithmUsedDescription.Type;
        var algorithm = CreateAlgorithm(algorithmType);
        return algorithm.Decrypt(encryptionResult);
    }

    private static ICryptoAlgorithm CreateAlgorithm(Type algorithmType) => 
        (ICryptoAlgorithm)Activator.CreateInstance(algorithmType)!;
}