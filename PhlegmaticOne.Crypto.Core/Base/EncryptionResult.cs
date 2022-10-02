namespace PhlegmaticOne.Crypto.Core.Base;

public class EncryptionResult<T> where T : IEncryptionData
{
	public string OriginalText { get; }
	public string EncryptedText { get; }
    public T EncyptionData { get; }
	public EncryptionResult(T encyptionData, string originalText, string encryptedText)
	{
		EncyptionData = encyptionData;
		OriginalText = originalText;
		EncryptedText = encryptedText;
	}
}
