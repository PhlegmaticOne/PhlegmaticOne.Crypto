namespace PhlegmaticOne.Crypto.Asymmetric.RSA.Helpers;

internal static class MathHelper
{
    public static int GetRandomPrime(List<int> primes)
    {
        var randomIndex = Random.Shared.Next(0, primes.Count);
        return primes[randomIndex];
    }

    public static List<int> GeneratePrimes(int limit)
    {
        var grid = new List<bool>();
        var primes = new List<int>();

        for (var i = 1; i < limit; i++)
        {
            grid.Add(true);
        }

        for (var i = 2; i < limit; i++)
        {
            if (!grid[i - 2]) continue;

            for (var j = i * 2; j <= limit; j += i)
            {
                grid[j - 2] = false;
            }
        }

        for (var p = 0; p < grid.Count; p++)
        {
            if (grid[p]) { primes.Add(p + 2); }
        }

        return primes;
    }
    public static long Gcd(long a, long b)
    {
        while (b != 0)
        {
            long tmp;
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
