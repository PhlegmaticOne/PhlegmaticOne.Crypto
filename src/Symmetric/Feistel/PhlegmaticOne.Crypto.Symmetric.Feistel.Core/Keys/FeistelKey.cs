using System.Collections;

namespace PhlegmaticOne.Crypto.Symmetric.Feistel.Core.Keys;

public class FeistelKey
{
	public BitArray Key { get; }
	public FeistelKey(BitArray key)
	{
		Key = key;
	}
}
