using PhlegmaticOne.Crypto.AlgorithmsExecution;
using PhlegmaticOne.Crypto.Asymmetric.RSA;
using PhlegmaticOne.Crypto.Asymmetric.RSA.EncryptionData;
using PhlegmaticOne.Crypto.ClassicCrypto.DigitalCryptography;
using PhlegmaticOne.Crypto.ClassicCrypto.DigitalCryptography.EncryptionData;
using PhlegmaticOne.Crypto.ClassicCrypto.DigitalCryptography.LettersEncryption;
using PhlegmaticOne.Crypto.ClassicCrypto.PolybiusSquare;
using PhlegmaticOne.Crypto.ClassicCrypto.PolybiusSquare.Alphabet;
using PhlegmaticOne.Crypto.ClassicCrypto.PolybiusSquare.EncryptionData;
using PhlegmaticOne.Crypto.ClassicCrypto.PolybiusSquare.LettersEncryption;
using PhlegmaticOne.Crypto.Core.Alphabet;
using PhlegmaticOne.Crypto.Symmetric.CardanoGrid;
using PhlegmaticOne.Crypto.Symmetric.Gamma;
using PhlegmaticOne.Crypto.Symmetric.Polynomial;
using PhlegmaticOne.Crypto.Core.Base;
using PhlegmaticOne.Crypto.Symmetric.CardanoGrid.EncryptionData;
using PhlegmaticOne.Crypto.Symmetric.CardanoGrid.Masks;
using PhlegmaticOne.Crypto.Symmetric.Feistel;
using PhlegmaticOne.Crypto.Symmetric.Feistel.EncryptionData;
using PhlegmaticOne.Crypto.Symmetric.Feistel.Functions;
using PhlegmaticOne.Crypto.Symmetric.Feistel.Keys;
using PhlegmaticOne.Crypto.Symmetric.Gamma.EncryptionData;
using PhlegmaticOne.Crypto.Symmetric.Gamma.KeyGenerators;
using PhlegmaticOne.Crypto.Symmetric.Polynomial.EncryptionData;


Console.InputEncoding = System.Text.Encoding.Unicode;
Console.OutputEncoding = System.Text.Encoding.Unicode;


var algorithmsExecutor = new ConfiguringAlgorithmsCryptoAlgorithmsExecutor(new List<ICryptoAlgorithm>
{
    new DigitalCryptographyAlgorithm(),
    new PolybiusSquareAlgorithm(),
    new GammaAlgorithm(),
    new PolynomialAlgorithm(),
    new CardanoGridAlgorithm(),
    new SymmetricFeistelAlgorithm(),
    new RsaAlgorithm()
});

var isExitRequested = false;
while (isExitRequested == false)
{
    Console.WriteLine("Введите строку для шифрования: ");

    var stringToEncrypt = Console.ReadLine()!;

    if (stringToEncrypt == "0")
    {
        isExitRequested = true;
        continue;
    }

    Console.WriteLine("\nВыберите алгоритм шифрования (введите номер) (0 - выход):");
    Console.WriteLine("1) Цифровая система тайнописи");
    Console.WriteLine("2) Квадрат Полибия");
    Console.WriteLine("3) Гаммирование");
    Console.WriteLine("4) Метод полиномов");
    Console.WriteLine("5) Решетка Кардано");
    Console.WriteLine("6) Абсолютно симметричная сеть Фейстеля");
    Console.WriteLine("7) Rsa-алгоритм");

    var num = int.Parse(Console.ReadLine()!);
    var encryptionData = CreateEncryptionData(num);

    if (encryptionData is null)
    {
        Console.WriteLine("Попробуйте снова");
        continue;
    }

    var encrypted = algorithmsExecutor.Encrypt(stringToEncrypt, encryptionData);
    var decrypted = algorithmsExecutor.Decrypt(encrypted);

    Console.WriteLine();
    Console.WriteLine(decrypted.AlgorithmUsedDescription.Description);
    Console.Write("\nOriginal text: ");
    Console.WriteLine(decrypted.OriginalText);
    Console.Write("Encrypted text: ");
    Console.WriteLine(decrypted.EncryptedText);
    Console.Write("Decrypted text: ");
    Console.WriteLine(decrypted.DecryptedText);
    Console.WriteLine();
}

static IEncryptionData? CreateEncryptionData(int number)
{
    IEncryptionData? result = default;
    const string alphabetString = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя ";

    switch (number)
    {
        case 1:
        {
            var alphabet = new Dictionary<char, int>
            {
                { 'а', 1 }, { 'б', 2 }, { 'в', 3 }, { 'г', 4 }, { 'д', 5 }, { 'е', 6 }, { 'ж', 7 }, { 'з', 8 },
                { 'и', 10 }, { 'й', 20 }, { 'к', 30 }, { 'л', 40 }, { 'м', 50 }, { 'н', 60 }, { 'о', 70 }, { 'п', 80 },
                { 'р', 100 }, { 'с', 200 }, { 'т', 300 }, { 'у', 400 }, { 'ф', 500 }, { 'х', 600 }, { 'ц', 700 }, { 'ч', 800 },
                { 'ш', 1000 }, { 'щ', 2000 }, { 'ъ', 3000 }, { 'ы', 4000 }, { 'ь', 5000 }, { 'э', 6000 }, { 'ю', 7000 }, { 'я', 8000 },
            };

            const char separateValue = '.';
            var letterDigitConverter = new LetterToDigitConverter(alphabet);
            var letterEncryptionPolicy = new SplitToMaxSymmetryWithTwoSizeEncryptionPolicy(letterDigitConverter);

            result = new DigitalAlgorithmData(letterEncryptionPolicy, separateValue);
            break;
        }
        case 2:
        {
            var squareAlphabet = SquareAlphabet.FromAlphabet(alphabetString);
            var letterEncryptionPolicy = new OneRowDownEncryptionPolicy(squareAlphabet);
            result = new PolybiusSquareEncryptionData(letterEncryptionPolicy);
            break;
        }
        case 3:
        {
            var letterToDigitConverter = LetterToDigitConverter.FromAlphabetString(alphabetString);
            var keyGenerator = new RandomKeyGenerator();
            result = new GammaAlgorithmEncryptionData(letterToDigitConverter, keyGenerator, letterToDigitConverter.Length);
            break;
        }
        case 4:
        {
            var letterToDigitConverter = LetterToDigitConverter.FromAlphabetString(alphabetString);

            int PolynomialFunc(int x) => x * x * x + 2 * x * x + 3 * x + 4;
            const int mod = 911;
            const char separatingChar = ' ';
            result = new PolynomialAlgorithmEncryptionData(letterToDigitConverter, PolynomialFunc, mod, separatingChar);
            break;
        }
        case 5:
        {
            var letterToDigitConverter = LetterToDigitConverter.FromAlphabetString(alphabetString);
            var maskGenerator = new RandomMaskGenerator();
            result = new CardanoGridAlgorithmEncryptionData(maskGenerator, letterToDigitConverter);
            break;
        }
        case 6:
        {
            var keyGenerator = new RandomInitialKeyGenerator();
            var roundKeyGenerator = new ShiftRoundKeyGenerator();
            var function = new OrFeistelFunction();
            var postFunction = new PBoxFunction();
            result = new FeistelAlgorithmData(keyGenerator,
                roundKeyGenerator, function,
                new List<IPostFeistelFunction> { postFunction },
                32, 64);
                break;
        }
        case 7:
        {
            const char separatingChar = ' ';
            const int primeNumbersLimitation = 500;
            var alphabet = LetterToDigitConverter.FromAlphabetString(alphabetString);
            result = new RsaEncryptionData(alphabet, primeNumbersLimitation, separatingChar);
            break;
        }
    }

    return result;
}