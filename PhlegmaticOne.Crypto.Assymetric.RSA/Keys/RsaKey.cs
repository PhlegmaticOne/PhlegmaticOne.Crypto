namespace PhlegmaticOne.Crypto.Asymmetric.RSA.Keys;

public class RsaKey
{
    public long FirstMagicNumber { get; }
    public long SecondMagicNumber { get; }
    public RsaKey(long firstMagicNumber, long secondMagicNumber)
    {
        FirstMagicNumber = firstMagicNumber;
        SecondMagicNumber = secondMagicNumber;
    }
}
