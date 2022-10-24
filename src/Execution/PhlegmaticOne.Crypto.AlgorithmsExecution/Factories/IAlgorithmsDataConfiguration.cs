using PhlegmaticOne.Crypto.Core.Base;

namespace PhlegmaticOne.Crypto.AlgorithmsExecution.Factories;

public interface IAlgorithmsDataConfiguration
{
    IEncryptionData GetAlgorithmData(Type algorithmType);
}