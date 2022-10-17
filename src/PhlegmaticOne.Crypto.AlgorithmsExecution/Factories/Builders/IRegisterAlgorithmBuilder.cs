using PhlegmaticOne.Crypto.Core.Base;

namespace PhlegmaticOne.Crypto.AlgorithmsExecution.Factories.Builders;

public interface IRegisterAlgorithmBuilder<TData> where TData : IEncryptionData
{
    AlgorithmsExecutingConfigurationBuilder RegisterAlgorithm<TAlgorithm>()
        where TAlgorithm : ICryptoAlgorithm<TData>;
}