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

public class EncryptionResult
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
}
