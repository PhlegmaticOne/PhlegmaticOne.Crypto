using System.Collections;

namespace PhlegmaticOne.Crypto.Symmetric.Feistel.Keys;

public class FeistelKey
{
	public BitArray Key { get; } = null!;
	public FeistelKey(BitArray key)
	{
		Key = key;
	}
}
