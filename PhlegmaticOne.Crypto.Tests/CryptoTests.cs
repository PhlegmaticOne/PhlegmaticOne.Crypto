using PhlegmaticOne.Crypto.Assymetric.RSA;
using PhlegmaticOne.Crypto.Assymetric.RSA.EncrypitionData;
using PhlegmaticOne.Crypto.ClassicCrypto.Core.LettersEncyption;
using PhlegmaticOne.Crypto.Core.Alphabet;
using PhlegmaticOne.Crypto.Core.Base;
using PhlegmaticOne.Crypto.DigitalCryptography;
using PhlegmaticOne.Crypto.DigitalCryptography.EncryptionData;
using PhlegmaticOne.Crypto.DigitalCryptography.LettersEncryption;
using PhlegmaticOne.Crypto.Gamma;
using PhlegmaticOne.Crypto.PolybiusSquare;
using PhlegmaticOne.Crypto.PolybiusSquare.Alphabet;
using PhlegmaticOne.Crypto.PolybiusSquare.EncryptionData;
using PhlegmaticOne.Crypto.PolybiusSquare.LettersEncryption;
using PhlegmaticOne.Crypto.Symmetric.CardanoGrid;
using PhlegmaticOne.Crypto.Symmetric.CardanoGrid.EncryptionData;
using PhlegmaticOne.Crypto.Symmetric.CardanoGrid.Grid;
using PhlegmaticOne.Crypto.Symmetric.CardanoGrid.Masks;
using PhlegmaticOne.Crypto.Symmetric.Gamma.EncryptionData;
using PhlegmaticOne.Crypto.Symmetric.Gamma.KeyGenerators;
using PhlegmaticOne.Crypto.Symmetric.Polynomial;
using PhlegmaticOne.Crypto.Symmetric.Polynomial.EncryptionData;

namespace PhlegmaticOne.Crypto.Tests;

public class CryptoTests
{
    [Fact]
    public void DigitalCryptography_Test()
    {
        var alphabet = new Dictionary<char, int>
        {
            { 'а', 1 },
            { 'б', 2 },
            { 'в', 3 },
            { 'г', 4 },
            { 'д', 5 },
            { 'е', 6 },
            { 'ж', 7 },
            { 'з', 8 },
            { 'и', 10 },
            { 'й', 20 },
            { 'к', 30 },
            { 'л', 40 },
            { 'м', 50 },
            { 'н', 60 },
            { 'о', 70 },
            { 'п', 80 },
            { 'р', 100 },
            { 'с', 200 },
            { 'т', 300 },
            { 'у', 400 },
            { 'ф', 500 },
            { 'х', 600 },
            { 'ц', 700 },
            { 'ч', 800 },
            { 'ш', 1000 },
            { 'щ', 2000 },
            { 'ъ', 3000 },
            { 'ы', 4000 },
            { 'ь', 5000 },
            { 'э', 6000 },
            { 'ю', 7000 },
            { 'я', 8000 },
        };

        char separateValue = '.';
        ILetterToDigitConverter letterDigitConverter = new LetterToDigitConverter(alphabet);
        ILetterEncryptionPolicy letterEncryptionPolicy = new SplitToMaxSymmetryWithTwoSizeEncryptionPolicy(letterDigitConverter);
        ICryptoAlgorithm<DigitalAlgorithmData> cryptoAlgorithm = new DigitalCryptographyAlgorithm();

        var encryptionData = new DigitalAlgorithmData(letterEncryptionPolicy, separateValue);

        var testString = "кротов александр вячеславович";

        var encrypted = cryptoAlgorithm.Encrypt(testString, encryptionData);

        var decrypted = cryptoAlgorithm.Decrypt(encrypted);

        Assert.Equal(decrypted.OriginalText, decrypted.DecryptedText);
    }

    [Fact]
    public void PolibiusSquareAlgorithm_Test()
    {
        var alphabet = "абвгдежзийклмнопрстуфхцчшщъыьэюя ";
        var squareAlphabet = SquareAlphabet.FromAlphabet(alphabet);
        var letterEncryptionPolicy = new OneRowDownEncryptionPolicy(squareAlphabet);

        var cryptoAlgorithm = new PolibiusSquareAlgorithm();

        var testString = "кротов александр вячеславович";
        var encryptionData = new PolybiusSquareEncryptionData(letterEncryptionPolicy);

        var encrypted = cryptoAlgorithm.Encrypt(testString, encryptionData);

        var decrypted = cryptoAlgorithm.Decrypt(encrypted);

        Assert.Equal(decrypted.OriginalText, decrypted.DecryptedText);
    }

    [Fact]
    public void PolibiusSquareAlgorithm_Test1()
    {
        var alphabet = "абвгдежзийклмнопрстуфхцчшщъыьэюя";
        var english = "abcdefghiklmnopqrstuvwxyz";
        var testString = "sometext";
        var squareAlphabet = SquareAlphabet.FromAlphabet(english);
        var letterEncryptionPolicy = new ReadPolibiusEncryptedDigitCodesByRowsLetterEncryptionPolicy(squareAlphabet);

        var cryptoAlgorithm = new PolibiusSquareAlgorithm();

        var encryptionData = new PolybiusSquareEncryptionData(letterEncryptionPolicy);

        var encrypted = cryptoAlgorithm.Encrypt(testString, encryptionData);

        var decrypted = cryptoAlgorithm.Decrypt(encrypted);

        Assert.Equal(decrypted.OriginalText, decrypted.DecryptedText);
    }

    [Fact]
    public void GammaAlgorithm_Test()
    {
        var alphabetString = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя ";
        var alphabet = alphabetString.Select((x, i) => (x, i)).ToDictionary(x => x.x, x => x.i);
        var letterToDigitConverter = new LetterToDigitConverter(alphabet);
        var keyGenerator = new RandomKeyGenerator();

        var encryptionData = new GammaAlgorithmEncryptionData(letterToDigitConverter, keyGenerator, alphabet.Count);
        var cryptoAlgorithm = new GammaAlgorithm();
        var testString = "радиотехника";

        var encrypted = cryptoAlgorithm.Encrypt(testString, encryptionData);

        var decrypted = cryptoAlgorithm.Decrypt(encrypted);

        Assert.Equal(decrypted.OriginalText, decrypted.DecryptedText);
    }

    [Fact]
    public void PolynomialAlgorithm_Test()
    {
        var alphabetString = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        var alphabet = alphabetString.Select((x, i) => (x, i)).ToDictionary(x => x.x, x => x.i);
        var letterToDigitConverter = new LetterToDigitConverter(alphabet);

        var polynomialFunc = (int x) => x * x * x + 2 * x * x + 3 * x + 4;
        var mod = 911;
        var separatingChar = ' ';
        var encryptionData = new PolynomialAlgorithmEncryptionData(letterToDigitConverter, polynomialFunc, mod, separatingChar);
        var cryptoAlgorithm = new PolynomialAlgorithm();
        var testString = "радиотехника";

        var encrypted = cryptoAlgorithm.Encrypt(testString, encryptionData);

        var decrypted = cryptoAlgorithm.Decrypt(encrypted);

        Assert.Equal(decrypted.OriginalText, decrypted.DecryptedText);
    }

    [Fact]
    public void CardanoGridAlgorithm_Test()
    {
        var alphabetString = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя ";
        var alphabet = alphabetString.Select((x, i) => (x, i)).ToDictionary(x => x.x, x => x.i);
        var letterToDigitConverter = new LetterToDigitConverter(alphabet);
        var maskGenerator = new RandomMaskGenerator();
        var encryptionData = new CardanoGridAlgorithmEncryptionData(maskGenerator, letterToDigitConverter);
        var algorithm = new CardanoGridAlgorithm();

        var str = "кротов александр вячеславович";
        var encrypted = algorithm.Encrypt(str, encryptionData);
        var decrypted = algorithm.Decrypt(encrypted);

        Assert.Equal(str, decrypted.DecryptedText);
    }

    [Fact]
    public void GeneratePrimes_Test()
    {
        var alphabetString = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя ";
        var alphabet = alphabetString.Select((x, i) => (x, i)).ToDictionary(x => x.x, x => x.i);
        var letterToDigitConverter = new LetterToDigitConverter(alphabet);

        var encryptionData = new RsaEncryptionData(letterToDigitConverter, 500, ' ');
        var algorithm = new RsaAlgorithm();

        var testString = "кротов александр вячеславович";

        var encrypted = algorithm.Encrypt(testString, encryptionData);

        var decrypted = algorithm.Decrypt(encrypted);

        Assert.Equal(testString, decrypted.DecryptedText);
    }
}