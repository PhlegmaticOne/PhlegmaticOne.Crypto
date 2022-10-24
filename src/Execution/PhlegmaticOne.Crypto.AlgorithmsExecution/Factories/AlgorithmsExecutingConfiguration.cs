using PhlegmaticOne.Crypto.Core.Base;

namespace PhlegmaticOne.Crypto.AlgorithmsExecution.Factories;

public class AlgorithmsExecutingConfiguration : IAlgorithmsDataConfiguration
{
    private readonly Dictionary<Type, Func<IEncryptionData>> _dataFactories;

    public AlgorithmsExecutingConfiguration(Dictionary<Type, Func<IEncryptionData>> dataFactories)
    {
        _dataFactories = dataFactories;
    }
    public IEncryptionData GetAlgorithmData(Type algorithmType)
    {
        if (_dataFactories.TryGetValue(algorithmType, out var factoryFunc))
        {
            return factoryFunc();
        }

        throw new ArgumentException();
    }
}