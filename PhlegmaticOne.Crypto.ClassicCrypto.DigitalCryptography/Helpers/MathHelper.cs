namespace PhlegmaticOne.Crypto.ClassicCrypto.DigitalCryptography.Helpers;

internal static class MathHelper
{
    public static int TenInLow10Exponent(int value)
    {
        var lowExponent = (int)Math.Log10(value);
        return (int)Math.Pow(10, lowExponent);
    }
}
