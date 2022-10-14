using PhlegmaticOne.Crypto.ClassicCrypto.Core.LettersEncyption;
using PhlegmaticOne.Crypto.Core.Alphabet;
using PhlegmaticOne.Crypto.Core.Extension;
using PhlegmaticOne.Crypto.Core.Helpers;
using PhlegmaticOne.Crypto.DigitalCryptography.Helpers;

namespace PhlegmaticOne.Crypto.DigitalCryptography.LettersEncryption;

public class SplitToMaxSymmetryWithTwoSizeEncryptionPolicy : ILetterEncryptionPolicy
{
    private readonly ILetterToDigitConverter _letterToDigitConverter;
    public SplitToMaxSymmetryWithTwoSizeEncryptionPolicy(ILetterToDigitConverter letterToDigitConverter)
    {
        _letterToDigitConverter = letterToDigitConverter;
    }
    public string EncryptLetter(char letter)
    {
        if(letter == CharConstants.SPACE) //Если буква - пробел
        {
            return Repeat(CharConstants.SPACE, 1); //Возвращаем строку - пробел
        }

        //Находим код буквы и наименьшую букву из диапазона
        var letterDigit = _letterToDigitConverter.ConvertLetter(letter);
        var tenInLowExponent = MathHelper.TenInLow10Exponent(letterDigit);
        var lowestLetter = _letterToDigitConverter.ConvertDigit(tenInLowExponent);

        //Если буква есть наименьшая буква, то возвоащаем ее
        if (letter == lowestLetter)
        {
            return Repeat(letter, 1);
        }

        //Находим все буквы в диапазоне
        var lettersInRange = _letterToDigitConverter.GetLettersInDigitRange(tenInLowExponent, letterDigit);


        //Если букв нечетное кол-во, то берем среднюю букву и возвращаем их две
        if (lettersInRange.Count.IsOdd() == false)
        {
            var medianLetter = lettersInRange.Skip(lettersInRange.Count / 2).Take(1).Single().Key;
            return Repeat(medianLetter, 2);
        }

        //Берем две буквы посередине, которые дают в сумме код исходной буквы
        var chars = lettersInRange.Skip(lettersInRange.Count / 2 - 1).Take(2).Select(x => x.Key).ToArray();
        return new string(chars);
    }

    public char DecryptLetter(string from)
    {
        var str = new string(from.ToArray());
        //Если буква пробел, то возвращаем его
        if (str == string.Empty)
        {
            return CharConstants.SPACE;
        }
        //Суммируем коды букв
        var digitResult = str.Select(_letterToDigitConverter.ConvertLetter).Sum();
        //Возвращаем букву с кодом
        return _letterToDigitConverter.ConvertDigit(digitResult);
    }

    public void PreEncryptAction(string stringToEncrypt) { }

    public void PreDecryptAction(string stringToDecrypt) { }
    private static string Repeat(char value, int times)
    {
        var array = Enumerable.Repeat(value, times).ToArray();
        return new string(array);
    }
}
