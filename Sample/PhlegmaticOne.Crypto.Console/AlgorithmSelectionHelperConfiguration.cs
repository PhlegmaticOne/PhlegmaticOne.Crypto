using PhlegmaticOne.Crypto.Core.Base;

namespace PhlegmaticOne.Crypto.Console;

public class AlgorithmSelectionHelperConfiguration
{
    private readonly Dictionary<int, ICryptoAlgorithm> _algorithmTypes;

    public AlgorithmSelectionHelperConfiguration(IReadOnlyList<ICryptoAlgorithm> cryptoAlgorithms)
    {
        _algorithmTypes = new();
        for (var i = 0; i < cryptoAlgorithms.Count; i++)
        {
            _algorithmTypes.Add(i + 1, cryptoAlgorithms[i]);
        }
    }

    public Type GetAlgorithmTypeByInput(int input) => _algorithmTypes[input].GetType();

    public IEnumerable<string> ToSelectionOptions() =>
        _algorithmTypes.Select(x => $"{x.Key}) {x.Value.Description}");
}