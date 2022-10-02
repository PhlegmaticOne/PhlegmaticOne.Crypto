namespace PhlegmaticOne.Crypto.Core.Base;

public class DecryptionResult
{
    public string OriginalText { get; set; }
    public string DecryptedText { get; set; }
    public string EncryptedText { get; set; }

    public DecryptionResult(string originalText, string decryptedText, string encryptedText)
    {
        OriginalText = originalText;
        DecryptedText = decryptedText;
        EncryptedText = encryptedText;
    }
}
