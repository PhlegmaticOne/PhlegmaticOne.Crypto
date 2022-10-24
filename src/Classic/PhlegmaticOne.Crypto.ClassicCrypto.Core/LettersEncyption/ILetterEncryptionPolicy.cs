namespace PhlegmaticOne.Crypto.ClassicCrypto.Core.LettersEncyption;

public interface ILetterEncryptionPolicy
{
    string EncryptLetter(char letter);
    char DecryptLetter(string from);
    void PreEncryptAction(string stringToEncrypt);
    void PreDecryptAction(string stringToDecrypt);
}