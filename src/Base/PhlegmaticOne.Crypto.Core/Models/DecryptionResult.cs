using PhlegmaticOne.Crypto.Core.Base;

namespace PhlegmaticOne.Crypto.Core.Models;

public class DecryptionResult
{
    public string OriginalText { get; init; }
    public string EncryptedText { get; init; }
    public string DecryptedText { get; init; }
    public AlgorithmDescription AlgorithmUsedDescription { get; init; }
    public DecryptionResult(string originalText, string encryptedText,
        string decryptedText, AlgorithmDescription algorithmUsedDescription)
    {
        OriginalText = originalText;
        DecryptedText = decryptedText;
        EncryptedText = encryptedText;
        AlgorithmUsedDescription = algorithmUsedDescription;
    }

    public static DecryptionResult FromEncryptionResult<T>(EncryptionResult<T> encryptionResult, 
        string decryptedText) where T: IEncryptionData =>
        new(encryptionResult.OriginalText, encryptionResult.EncryptedText, decryptedText,
            encryptionResult.AlgorithmUsedDescription);

    public override string ToString() => AlgorithmUsedDescription.ToString();
}