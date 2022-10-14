using PhlegmaticOne.Crypto.Core.Base;

namespace PhlegmaticOne.Crypto.Core.Models;

public class EncryptionResult<T> where T : IEncryptionData
{
    public string OriginalText { get; }
    public string EncryptedText { get; }
    public string AlgorithmUsedDescription { get; }
    public T EncryptionData { get; }
    public EncryptionResult(T encryptionData, string originalText, string encryptedText, string algorithmUsedDescription)
    {
        EncryptionData = encryptionData;
        OriginalText = originalText;
        EncryptedText = encryptedText;
        AlgorithmUsedDescription = algorithmUsedDescription;
    }
}
