using PhlegmaticOne.Crypto.ClassicCrypto.Core;
using PhlegmaticOne.Crypto.ClassicCrypto.Core.LettersEncyption;

namespace PhlegmaticOne.Crypto.ClassicCrypto.DigitalCryptography.EncryptionData;

public class DigitalAlgorithmData : ClassicCryptoEncryptionDataBase
{
    public char SeparatingEncryptedLettersChar { get; }
    public DigitalAlgorithmData(ILetterEncryptionPolicy letterEncryptionPolicy, char separatingEncryptedLettersChar)
        : base(letterEncryptionPolicy)
    {
        SeparatingEncryptedLettersChar = separatingEncryptedLettersChar;
    }
}
