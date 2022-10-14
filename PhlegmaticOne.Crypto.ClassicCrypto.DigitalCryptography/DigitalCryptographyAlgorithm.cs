using PhlegmaticOne.Crypto.Core.Base;
using PhlegmaticOne.Crypto.Core.Helpers;
using PhlegmaticOne.Crypto.DigitalCryptography.EncryptionData;
using System.Text;

namespace PhlegmaticOne.Crypto.DigitalCryptography;

public class DigitalCryptographyAlgorithm : ICryptoAlgorithm<DigitalAlgorithmData>
{
    public EncryptionResult<DigitalAlgorithmData> Encrypt(string textToEncrypt, DigitalAlgorithmData encryptionData)
    {
        var result = new StringBuilder();
        foreach (var letter in textToEncrypt)
        {
            //Шифруем букву
            var encrypted = encryptionData.LetterEncryptionPolicy.EncryptLetter(letter);
            //Добавляем ее в зашифрованную строку
            result.Append(encrypted);
            //Если буква не пробел, то добавляем разделительный знак между зашифрованными буквами
            if (letter != CharConstants.SPACE)
            {
                result.Append(encryptionData.SeparatingEncryptedLettersChar);
            }
        }

        var enprypted = result.ToString();
        return new EncryptionResult<DigitalAlgorithmData>(encryptionData, textToEncrypt, enprypted);
    }

    public DecryptionResult Decrypt(EncryptionResult<DigitalAlgorithmData> encryptionResult)
    {
        //Разбиваем зашифрованную строку на зашифрованные буквы
        var splitted = encryptionResult.EncryptedText
            .Split(encryptionResult.EncyptionData.SeparatingEncryptedLettersChar, CharConstants.SPACE);

        var result = new StringBuilder();
        foreach (var encrypted in splitted)
        {
            //Дешифруем каждую букву
            result.Append(encryptionResult.EncyptionData.LetterEncryptionPolicy.DecryptLetter(encrypted));
        }

        var decprypted = result.ToString().TrimEnd();
        return new(encryptionResult.OriginalText, decprypted, encryptionResult.EncryptedText);
    }
}