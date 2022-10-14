using PhlegmaticOne.Crypto.Asymmetric.RSA.Helpers;
using PhlegmaticOne.Crypto.Asymmetric.RSA.Keys;
using PhlegmaticOne.Crypto.Core.Alphabet;
using PhlegmaticOne.Crypto.Core.Base;

namespace PhlegmaticOne.Crypto.Asymmetric.RSA.EncryptionData;

public class RsaEncryptionData : IEncryptionData
{
	private readonly List<int> _primes;
    public RsaKey PublicKey { get; private set; } = null!;
    public RsaKey SecretKey { get; private set; } = null!;
	public ILetterToDigitConverter Alphabet { get; }
	public char SeparatingChar { get; }

	public RsaEncryptionData(ILetterToDigitConverter alphabet, int primeNumbersLimitation, char separatingChar)
	{
		_primes = MathHelper.GeneratePrimes(primeNumbersLimitation);
		Alphabet = alphabet;
		SeparatingChar = separatingChar;
	}

    public void GenerateKeys()
    {
        var p = MathHelper.GetRandomPrime(_primes);
        var q = MathHelper.GetRandomPrime(_primes);

        long n = p * q;
		long m = (p - 1) * (q - 1);

		var d = GetD(m);
		var e = GetE(d, m);

        PublicKey = new(e, n);
        SecretKey = new(d, n);
    }

	private static long GetD(long m)
	{
		var d = Random.Shared.NextInt64(m / 2, m);

		for (var i = d - 1; i >= 2; i--)
		{
			if(MathHelper.Gcd(d, m) != 1)
			{
				d--;
			}
			else
			{
				break;
			}
		}

		return d;
    }

    private static long GetE(long d, long m)
    {
        long e = 10;

        while (true)
        {
            if ((e * d) % m == 1)
                break;
            else
                e++;
        }

        return e;
    }
}
