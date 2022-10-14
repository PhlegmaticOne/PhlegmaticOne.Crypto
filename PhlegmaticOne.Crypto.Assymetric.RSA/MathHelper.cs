namespace PhlegmaticOne.Crypto.Assymetric.RSA;

public static class MathHelper
{
    public static bool IsPrime(int number)
    {
        if (number % 2 == 0) return false;

        var sqrt = Convert.ToInt64(Math.Sqrt(number)) + 1;

        for (var i = 3; i < sqrt; i += 2)
        {
            if(number % i == 0)
            {
                return false;
            }
        }

        return true;
    }

    public static int GenerateRandomPrime(int limit)
    {
        var primes = GeneratePrimes(limit);

        var randomIndex = Random.Shared.Next(0, primes.Count);
        return primes[randomIndex];
    }

    public static int GetRandomPrime(List<int> primes)
    {
        var randomIndex = Random.Shared.Next(0, primes.Count);
        return primes[randomIndex];
    }

    public static List<int> GeneratePrimes(int limit)
    {
        var bools = new List<bool>();
        var primes = new List<int>();

        for (var i = 1; i < limit; i++)
        {
            bools.Add(true);
        }

        for (var i = 2; i < limit; i++)
        {
            if (bools[i - 2])
            {
                for (var j = i * 2; j <= limit; j += i)
                {
                    bools[j - 2] = false;
                }
            }
        }

        for (var p = 0; p < bools.Count; p++)
        {
            if (bools[p]) { primes.Add(p + 2); }
        }

        return primes;
    }
    public static long GCD(long a, long b)
    {
        long tmp;
        while (b != 0)
        {
            if (a < b)
            {
                tmp = a;
                a = b;
                b = tmp;
            }
            tmp = b;
            b = a % b;
            a = tmp;
        }
        return a;
    }
}
