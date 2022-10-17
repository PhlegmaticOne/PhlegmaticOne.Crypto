using System.Collections;

namespace PhlegmaticOne.Crypto.Symmetric.Feistel.Extensions;

public static class BitArrayExtensions
{
    public static BitArray ImmutableXor(this BitArray a, BitArray b)
    {
        if(a.Length != b.Length)
        {
            throw new ArgumentException("Microsoft sucks with modifying bitarrays in logical operations");
        }
        var result = new bool[a.Length];
        for (int i = 0; i < a.Length; i++)
        {
            result[i] = a[i] ^ b[i]; 
        }
        return new(result);
    }

    public static BitArray ImmutableOr(this BitArray a, BitArray b)
    {
        if (a.Length != b.Length)
        {
            throw new ArgumentException("Microsoft sucks with modifying bitarrays in logical operations");
        }
        var result = new bool[a.Length];
        for (int i = 0; i < a.Length; i++)
        {
            result[i] = a[i] | b[i];
        }
        return new(result);
    }

    public static BitArray ImmutableAnd(this BitArray a, BitArray b)
    {
        if (a.Length != b.Length)
        {
            throw new ArgumentException("Microsoft sucks with modifying bitarrays in logical operations");
        }
        var result = new bool[a.Length];
        for (int i = 0; i < a.Length; i++)
        {
            result[i] = a[i] & b[i];
        }
        return new(result);
    }

    public static byte[] ToByteArray(this BitArray bitArray)
    {
        var result = new byte[(bitArray.Length - 1) / 8 + 1];
        bitArray.CopyTo(result, 0);
        return result;
    }

    public static BitArray GetHalfBits(this BitArray bitArray, bool isFirstHalf)
    {
        var halfLength = bitArray.Length / 2;
        var result = new bool[halfLength];
        var startIndex = isFirstHalf ? 0 : bitArray.Length / 2;
        var endIndex = startIndex + halfLength;
        for (int i = startIndex; i < endIndex; i++)
        {
            result[i - startIndex] = bitArray[i];
        }
        return new BitArray(result);
    }

    public static BitArray ImmutableCycleLeftShift(this BitArray bitArray, int count)
    {
        if (count <= 0 || count > bitArray.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(count));
        }

        if (count == bitArray.Length)
        {
            return new(bitArray);
        }

        var result = new bool[bitArray.Length];
        var shifting = new bool[count];

        for (var i = bitArray.Length - 1; i >= bitArray.Length - count; i--)
        {
            shifting[bitArray.Length - i - 1] = bitArray[i];
        }

        for (var i = bitArray.Length - count - 1; i >= 0; i--)
        {
            result[i + count] = bitArray[i];
        }

        for (var i = count - 1; i >= 0; i--)
        {
            result[count - i - 1] = shifting[i];
        }

        return new(result);  
    }

    public static BitArray ImmutableCycleRightShift(this BitArray bitArray, int count)
    {
        if (count <= 0 || count > bitArray.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(count));
        }

        if (count == bitArray.Length)
        {
            return new(bitArray);
        }

        var result = new bool[bitArray.Length];
        var shifting = new bool[count];

        for (var i = 0; i < count; i++)
        {
            shifting[i] = bitArray[i];
        }

        for (var i = count; i < bitArray.Length; i++)
        {
            result[i - count] = bitArray[i];
        }

        for (var i = 0; i < count; i++)
        {
            result[bitArray.Length - count + i] = shifting[i];
        }

        return new(result);
    }
}
