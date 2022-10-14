namespace PhlegmaticOne.Crypto.Core.Models;

public class AlgorithmDescription
{
    public string Description { get; }
    public Type Type { get; }

    public AlgorithmDescription(string description, Type type)
    {
        Description = description;
        Type = type;
    }

    public override string ToString() => $"Algorithm type: {Type.Name}";
}