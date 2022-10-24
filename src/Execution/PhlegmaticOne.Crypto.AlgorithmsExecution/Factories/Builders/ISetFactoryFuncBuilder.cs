using PhlegmaticOne.Crypto.Core.Base;

namespace PhlegmaticOne.Crypto.AlgorithmsExecution.Factories.Builders;

public interface ISetFactoryFuncBuilder
{
    IRegisterAlgorithmBuilder<TData> WithAlgorithmDataFactory<TData>(Func<TData> dataFactoryFunc)
        where TData : IEncryptionData;
}