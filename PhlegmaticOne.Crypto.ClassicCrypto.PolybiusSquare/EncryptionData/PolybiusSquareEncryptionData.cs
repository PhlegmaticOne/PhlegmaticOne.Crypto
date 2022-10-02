using PhlegmaticOne.Crypto.ClassicCrypto.Core;
using PhlegmaticOne.Crypto.ClassicCrypto.Core.LettersEncyption;

namespace PhlegmaticOne.Crypto.PolybiusSquare.EncryptionData;

public class PolybiusSquareEncryptionData : ClassicCryptoEncryptionDataBase
{
    public PolybiusSquareEncryptionData(ILetterEncryptionPolicy letterEncryptionPolicy) : base(letterEncryptionPolicy)
    {
    }
}
