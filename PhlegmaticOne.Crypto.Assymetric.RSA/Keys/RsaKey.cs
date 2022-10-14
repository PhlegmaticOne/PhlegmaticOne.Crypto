namespace PhlegmaticOne.Crypto.Assymetric.RSA.Keys;

public class RsaKey
{
    public long FirstMagicNumber { get; set; }
    public long SecondMagicNumber { get; set; }
    public RsaKey(long firstMagicNumber, long secondMagicNumber)
    {
        FirstMagicNumber = firstMagicNumber;
        SecondMagicNumber = secondMagicNumber;
    }
}
