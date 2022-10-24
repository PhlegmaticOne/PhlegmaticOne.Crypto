using PhlegmaticOne.Crypto.ClassicCrypto.Core.LettersEncyption;
using PhlegmaticOne.Crypto.Core.Base;

namespace PhlegmaticOne.Crypto.ClassicCrypto.Core;

public class ClassicCryptoEncryptionDataBase : IEncryptionData
{
    public ILetterEncryptionPolicy LetterEncryptionPolicy { get; }
	public ClassicCryptoEncryptionDataBase(ILetterEncryptionPolicy letterEncryptionPolicy)
	{
		LetterEncryptionPolicy = letterEncryptionPolicy;
	}
}