using PhlegmaticOne.Crypto.AlgorithmsExecution;
using PhlegmaticOne.Crypto.Asymmetric.RSA;
using PhlegmaticOne.Crypto.ClassicCrypto.DigitalCryptography;
using PhlegmaticOne.Crypto.ClassicCrypto.PolybiusSquare;
using PhlegmaticOne.Crypto.Symmetric.CardanoGrid;
using PhlegmaticOne.Crypto.Symmetric.Gamma;
using PhlegmaticOne.Crypto.Symmetric.Polynomial;
using PhlegmaticOne.Crypto.Core.Base;
using PhlegmaticOne.Crypto.Symmetric.Feistel;
using PhlegmaticOne.Crypto.Symmetric.Feistel.EncryptionData;
using PhlegmaticOne.Crypto.Symmetric.Feistel.Functions;
using PhlegmaticOne.Crypto.Symmetric.Feistel.Keys;


Console.InputEncoding = System.Text.Encoding.Unicode;
Console.OutputEncoding = System.Text.Encoding.Unicode;

var algorithmTypes = new List<Type>
{
    typeof(RsaAlgorithm),
    typeof(DigitalCryptographyAlgorithm),
    typeof(PolybiusSquareAlgorithm),
    typeof(CardanoGridAlgorithm),
    typeof(GammaAlgorithm),
    typeof(PolynomialAlgorithm),
    typeof(SymmetricFeistelAlgorithm)
};

var algorithmsExecutor = new ConfiguringCryptoAlgorithmsExecutor(algorithmTypes);

var isExitRequested = false;
while (isExitRequested == false)
{
    Console.WriteLine("Введите строку: ");

    var stringToEncrypt = Console.ReadLine()!;
    if (stringToEncrypt == "0")
    {
        isExitRequested = true;
        continue;
    }

    Console.WriteLine("\nВыберите алгоритм шифрования (введите номер)");
    Console.WriteLine("1) Сеть фейстеля");

    var num = int.Parse(Console.ReadLine()!);
    var encryptionData = Create(num);

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

static IEncryptionData Create(int number)
{
    IEncryptionData result = default!;

    switch (number)
    {
        case 1:
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
    }

    return result;
}