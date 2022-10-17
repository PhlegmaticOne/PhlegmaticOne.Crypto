using PhlegmaticOne.Crypto.AlgorithmsExecution.Base;
using PhlegmaticOne.Crypto.AlgorithmsExecution.Factories;
using PhlegmaticOne.Crypto.Core.Models;

namespace PhlegmaticOne.Crypto.Console;

public class AlgorithmExecutionHelper
{
    private readonly ICryptoAlgorithmsExecutor _cryptoAlgorithmsExecutor;
    private readonly IAlgorithmsDataConfiguration _algorithmsDataConfiguration;
    private readonly AlgorithmSelectionHelperConfiguration _algorithmSelectionHelperConfiguration;

    public AlgorithmExecutionHelper(ICryptoAlgorithmsExecutor cryptoAlgorithmsExecutor, 
        IAlgorithmsDataConfiguration algorithmsDataConfiguration,
        AlgorithmSelectionHelperConfiguration algorithmSelectionHelperConfiguration)
    {
        _cryptoAlgorithmsExecutor = cryptoAlgorithmsExecutor;
        _algorithmsDataConfiguration = algorithmsDataConfiguration;
        _algorithmSelectionHelperConfiguration = algorithmSelectionHelperConfiguration;
    }

    public IEnumerable<string> ToSelectionOptions() => _algorithmSelectionHelperConfiguration.ToSelectionOptions();

    public EncryptionResult EncryptByInput(int inputNumber, string textToEncrypt)
    {
        var algorithmType = _algorithmSelectionHelperConfiguration.GetAlgorithmTypeByInput(inputNumber);
        var algorithmData = _algorithmsDataConfiguration.GetAlgorithmData(algorithmType);
        var encryptionResult = _cryptoAlgorithmsExecutor.Encrypt(textToEncrypt, algorithmData);
        return encryptionResult;
    }

    public DecryptionResult Decrypt(EncryptionResult encryptionResult)
    {
        return _cryptoAlgorithmsExecutor.Decrypt(encryptionResult);
    }
}