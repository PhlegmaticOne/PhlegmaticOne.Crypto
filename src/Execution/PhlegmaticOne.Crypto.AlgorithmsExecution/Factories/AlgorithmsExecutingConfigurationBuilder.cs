using PhlegmaticOne.Crypto.AlgorithmsExecution.Factories.Builders;
using PhlegmaticOne.Crypto.Core.Base;

namespace PhlegmaticOne.Crypto.AlgorithmsExecution.Factories;

public class AlgorithmsExecutingConfigurationBuilder : ISetFactoryFuncBuilder
{
    private readonly Dictionary<Type, Func<IEncryptionData>> _dataFactories;
    public AlgorithmsExecutingConfigurationBuilder() => 
        _dataFactories = new();

    public IRegisterAlgorithmBuilder<TData> WithAlgorithmDataFactory<TData>(Func<TData> dataFactoryFunc)
        where TData : IEncryptionData =>
        new RegisterAlgorithmBuilder<TData>(dataFactoryFunc, this);

    public IAlgorithmsDataConfiguration ToAlgorithmsDataConfiguration() => 
        new AlgorithmsExecutingConfiguration(_dataFactories);

    private class RegisterAlgorithmBuilder<TData> : IRegisterAlgorithmBuilder<TData> 
        where TData : IEncryptionData
    {
        private readonly Func<TData> _dataFactoryFunc;
        private readonly AlgorithmsExecutingConfigurationBuilder _algorithmsExecutingConfigurationBuilder;

        public RegisterAlgorithmBuilder(Func<TData> dataFactoryFunc,
            AlgorithmsExecutingConfigurationBuilder algorithmsExecutingConfigurationBuilder)
        {
            _dataFactoryFunc = dataFactoryFunc;
            _algorithmsExecutingConfigurationBuilder = algorithmsExecutingConfigurationBuilder;
        }
        public AlgorithmsExecutingConfigurationBuilder RegisterAlgorithm<TAlgorithm>()
            where TAlgorithm : ICryptoAlgorithm<TData>
        {
            var algorithmType = typeof(TAlgorithm);
            _algorithmsExecutingConfigurationBuilder._dataFactories.Add(algorithmType, () => _dataFactoryFunc());
            return _algorithmsExecutingConfigurationBuilder;
        }
    }
}