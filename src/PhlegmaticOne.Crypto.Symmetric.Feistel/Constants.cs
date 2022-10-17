namespace PhlegmaticOne.Crypto.Symmetric.Feistel;

internal static class Constants
{
    internal const int CHAR_SIZE_IN_BITS = sizeof(char) * 8;
    internal static int GetBytesCount(int sizeInBits) => sizeInBits * 2 / CHAR_SIZE_IN_BITS;
}
