using PhlegmaticOne.Crypto.AlgorithmsExecution;
using PhlegmaticOne.Crypto.Asymmetric.RSA;
using PhlegmaticOne.Crypto.ClassicCrypto.Core.LettersEncyption;
using PhlegmaticOne.Crypto.ClassicCrypto.DigitalCryptography;
using PhlegmaticOne.Crypto.ClassicCrypto.DigitalCryptography.EncryptionData;
using PhlegmaticOne.Crypto.ClassicCrypto.DigitalCryptography.LettersEncryption;
using PhlegmaticOne.Crypto.ClassicCrypto.PolybiusSquare;
using PhlegmaticOne.Crypto.ClassicCrypto.PolybiusSquare.Alphabet;
using PhlegmaticOne.Crypto.ClassicCrypto.PolybiusSquare.EncryptionData;
using PhlegmaticOne.Crypto.ClassicCrypto.PolybiusSquare.LettersEncryption;
using PhlegmaticOne.Crypto.Core.Alphabet;
using PhlegmaticOne.Crypto.Core.Models;
using PhlegmaticOne.Crypto.Symmetric.CardanoGrid;
using PhlegmaticOne.Crypto.Symmetric.CardanoGrid.EncryptionData;
using PhlegmaticOne.Crypto.Symmetric.CardanoGrid.Masks;
using PhlegmaticOne.Crypto.Symmetric.Gamma;
using PhlegmaticOne.Crypto.Symmetric.Gamma.EncryptionData;
using PhlegmaticOne.Crypto.Symmetric.Gamma.KeyGenerators;
using PhlegmaticOne.Crypto.Symmetric.Polynomial;
using PhlegmaticOne.Crypto.Symmetric.Polynomial.EncryptionData;

//Dictionary<int, Func<string, DecryptionResult>> _actions = new()
//{
//    { 1, DigtalEncrypting },
//    { 2, PolybiusEncrypting },
//    { 3, GammaEncrypting },
//    { 4, PolynomialEncrypting },
//    { 5, CardanoEncrypting }
//};

//Console.InputEncoding = System.Text.Encoding.Unicode;
//Console.OutputEncoding = System.Text.Encoding.Unicode;
//bool isExitRequested = false;
//while (isExitRequested == false)
//{
//    Console.WriteLine("Введите строку: ");

//    var stringToEncrypt = Console.ReadLine();
//    if(stringToEncrypt == "0")
//    {
//        isExitRequested = true;
//        continue;
//    }

//    Console.WriteLine("\nВыберите алгоритм шифрования (введите номер)");
//    Console.WriteLine("1) Цифровая система тайнописи");
//    Console.WriteLine("2) Квадрат Полибия");
//    Console.WriteLine("3) Гаммирование");
//    Console.WriteLine("4) Метод полиномов");
//    Console.WriteLine("5) Решетка Кардано");

//    var num = int.Parse(Console.ReadLine()!);
//    if (_actions.TryGetValue(num, out var action))
//    {
//        var result = action.Invoke(stringToEncrypt!);

//        Console.WriteLine("Оригинальная строка: {0}", result.OriginalText);
//        Console.WriteLine("Зашифрованная строка: {0}", result.EncryptedText);
//        Console.WriteLine("Дешифрованная строка: {0}", result.DecryptedText);
//    }
//}
//static DecryptionResult DigtalEncrypting(string textToEncrypt)
//{
//    var alphabet = new Dictionary<char, int>
//        {
//            { 'а', 1 },
//            { 'б', 2 },
//            { 'в', 3 },
//            { 'г', 4 },
//            { 'д', 5 },
//            { 'е', 6 },
//            { 'ж', 7 },
//            { 'з', 8 },
//            { 'и', 10 },
//            { 'й', 20 },
//            { 'к', 30 },
//            { 'л', 40 },
//            { 'м', 50 },
//            { 'н', 60 },
//            { 'о', 70 },
//            { 'п', 80 },
//            { 'р', 100 },
//            { 'с', 200 },
//            { 'т', 300 },
//            { 'у', 400 },
//            { 'ф', 500 },
//            { 'х', 600 },
//            { 'ц', 700 },
//            { 'ч', 800 },
//            { 'ш', 1000 },
//            { 'щ', 2000 },
//            { 'ъ', 3000 },
//            { 'ы', 4000 },
//            { 'ь', 5000 },
//            { 'э', 6000 },
//            { 'ю', 7000 },
//            { 'я', 8000 },
//        };

//    char separateValue = '.';
//    var letterDigitConverter = new LetterToDigitConverter(alphabet);
//    var letterEncryptionPolicy = new SplitToMaxSymmetryWithTwoSizeEncryptionPolicy(letterDigitConverter);
//    var cryptoAlgorithm = new DigitalCryptographyAlgorithm();

//    var encryptionData = new DigitalAlgorithmData(letterEncryptionPolicy, separateValue);

//    var encrypted = cryptoAlgorithm.Encrypt(textToEncrypt, encryptionData);
//    var decrypted = cryptoAlgorithm.Decrypt(encrypted);

//    return decrypted;
//}

//static DecryptionResult PolybiusEncrypting(string textToEncrypt)
//{
//    var alphabet = "абвгдежзийклмнопрстуфхцчшщъыьэюя ";
//    var squareAlphabet = SquareAlphabet.FromAlphabet(alphabet);
//    var letterEncryptionPolicy = new OneRowDownEncryptionPolicy(squareAlphabet);

//    var cryptoAlgorithm = new PolybiusSquareAlgorithm();

//    var encryptionData = new PolybiusSquareEncryptionData(letterEncryptionPolicy);

//    var encrypted = cryptoAlgorithm.Encrypt(textToEncrypt, encryptionData);

//    var decrypted = cryptoAlgorithm.Decrypt(encrypted);
//    return decrypted;
//}

//static DecryptionResult GammaEncrypting(string textToEncrypt)
//{
//    var alphabetString = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя ";
//    var alphabet = alphabetString.Select((x, i) => (x, i)).ToDictionary(x => x.x, x => x.i);
//    var letterToDigitConverter = new LetterToDigitConverter(alphabet);
//    var keyGenerator = new RandomKeyGenerator();

//    var encryptionData = new GammaAlgorithmEncryptionData(letterToDigitConverter, keyGenerator, alphabet.Count);
//    var cryptoAlgorithm = new GammaAlgorithm();

//    var encrypted = cryptoAlgorithm.Encrypt(textToEncrypt, encryptionData);

//    var decrypted = cryptoAlgorithm.Decrypt(encrypted);
//    return decrypted;
//}

//static DecryptionResult PolynomialEncrypting(string textToEncrypt)
//{
//    var alphabetString = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя ";
//    var alphabet = alphabetString.Select((x, i) => (x, i)).ToDictionary(x => x.x, x => x.i);
//    var letterToDigitConverter = new LetterToDigitConverter(alphabet);

//    var polynomialFunc = (int x) => x * x * x + 2 * x * x + 3 * x + 4;
//    var mod = 911;
//    var separatingChar = ' ';
//    var encryptionData = new PolynomialAlgorithmEncryptionData(letterToDigitConverter, polynomialFunc, mod, separatingChar);
//    var cryptoAlgorithm = new PolynomialAlgorithm();

//    var encrypted = cryptoAlgorithm.Encrypt(textToEncrypt, encryptionData);

//    var decrypted = cryptoAlgorithm.Decrypt(encrypted);
//    return decrypted;
//}

//static DecryptionResult CardanoEncrypting(string textToEncrypt)
//{
//    var alphabetString = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя ";
//    var alphabet = alphabetString.Select((x, i) => (x, i)).ToDictionary(x => x.x, x => x.i);
//    var letterToDigitConverter = new LetterToDigitConverter(alphabet);
//    var maskGenerator = new RandomMaskGenerator();
//    var encryptionData = new CardanoGridAlgorithmEncryptionData(maskGenerator, letterToDigitConverter);
//    var algorithm = new CardanoGridAlgorithm();

//    var encrypted = algorithm.Encrypt(textToEncrypt, encryptionData);
//    var decrypted = algorithm.Decrypt(encrypted);
//    return decrypted;
//}

var algorithmTypes = new List<Type>
{
    typeof(RsaAlgorithm),
    typeof(DigitalCryptographyAlgorithm),
    typeof(PolybiusSquareAlgorithm),
    typeof(CardanoGridAlgorithm),
    typeof(GammaAlgorithm),
    typeof(PolynomialAlgorithm)
};

var algorithmsExecutor = new ConfiguringCryptoAlgorithmsExecutor(algorithmTypes);


var squareAlphabet = SquareAlphabet.FromAlphabet("абвгдежзийклмнопрстуфхцчшщъыьэюя ");
var encryptionPolicy = new OneRowDownEncryptionPolicy(squareAlphabet);
var algorithmData = new PolybiusSquareEncryptionData(encryptionPolicy);

var encrypted = algorithmsExecutor.Encrypt("диспетчер", algorithmData);

var decrypted = algorithmsExecutor.Decrypt(encrypted);

Console.WriteLine(decrypted);