using PhlegmaticOne.Crypto.Core.Base;

namespace PhlegmaticOne.Crypto.Core.Models;

public class EncryptionResult<T> : EncryptionResult where T : IEncryptionData
{
    public T EncryptionData { get; }
    public EncryptionResult(T encryptionData, string originalText, string encryptedText, string algorithmUsedDescription, Type type) : 
        base(originalText, encryptedText, algorithmUsedDescription, type)
    {
        EncryptionData = encryptionData;
    }
}

public abstract class EncryptionResult
{
    public string OriginalText { get; }
    public string EncryptedText { get; }
    public AlgorithmDescription AlgorithmUsedDescription { get; }
    public EncryptionResult(string originalText, string encryptedText, string algorithmUsedDescription, Type algorithmUsedType)
    {
        OriginalText = originalText;
        EncryptedText = encryptedText;
        AlgorithmUsedDescription = new(algorithmUsedDescription, algorithmUsedType);
    }
    public bool TryGetEncryptionData<T>(out T? data) where T : IEncryptionData
    {
        data = default;
        if(this is EncryptionResult<T> generic)
        {
            data = generic.EncryptionData;
            return true;
        }
        return false;
    }
    public IEncryptionData GetCommonEncryptionData()
    {
        var dataProperty = GetType().GetProperty("EncryptionData")!;
        return (IEncryptionData)dataProperty.GetValue(this)!;
    }
    public override string ToString() => EncryptedText;
}
