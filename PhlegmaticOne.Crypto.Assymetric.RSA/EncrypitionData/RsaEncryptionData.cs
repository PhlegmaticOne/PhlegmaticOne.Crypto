using PhlegmaticOne.Crypto.Assymetric.RSA.Keys;
using PhlegmaticOne.Crypto.Core.Alphabet;
using PhlegmaticOne.Crypto.Core.Base;

namespace PhlegmaticOne.Crypto.Assymetric.RSA.EncrypitionData;

public class RsaEncryptionData : IEncryptionData
{
	private readonly List<int> _primes;
	public RsaKey PublicKey { get; set; }
	public RsaKey SecretKey { get; set; }
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

		long d = GetD(m);
		long e = GetE(d, m);

        PublicKey = new(e, n);
        SecretKey = new(d, n);
    }

	private long GetD(long m)
	{
		long d = Random.Shared.NextInt64(m / 2, m);

		for (long i = d - 1; i >= 2; i--)
		{
			if(MathHelper.GCD(d, m) != 1)
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

    private long GetE(long d, long m)
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
