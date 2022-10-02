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
        if(letter == CharConstants.SPACE)
        {
            return Repeat(CharConstants.SPACE, 1);
        }

        var letterDigit = _letterToDigitConverter.ConvertLetter(letter);
        var tenInLowExponent = MathHelper.TenInLow10Exponent(letterDigit);
        var lowestLetter = _letterToDigitConverter.ConvertDigit(tenInLowExponent);

        if (letter == lowestLetter)
        {
            return Repeat(letter, 1);
        }

        var lettersInRange = _letterToDigitConverter.GetLettersInDigitRange(tenInLowExponent, letterDigit);


        if (lettersInRange.Count.IsOdd() == false)
        {
            var medianLetter = lettersInRange.Skip(lettersInRange.Count / 2).Take(1).Single().Key;
            return Repeat(medianLetter, 2);
        }

        var chars = lettersInRange.Skip(lettersInRange.Count / 2 - 1).Take(2).Select(x => x.Key).ToArray();
        return new string(chars);
    }

    public char DecryptLetter(string from)
    {
        var str = new string(from.ToArray());
        if (str == string.Empty)
        {
            return CharConstants.SPACE;
        }
        var digitResult = str.Select(_letterToDigitConverter.ConvertLetter).Sum();
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
