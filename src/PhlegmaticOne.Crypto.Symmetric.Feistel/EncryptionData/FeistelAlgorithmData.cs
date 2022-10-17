using PhlegmaticOne.Crypto.Core.Base;
using PhlegmaticOne.Crypto.Symmetric.Feistel.Functions;
using PhlegmaticOne.Crypto.Symmetric.Feistel.Keys;
using System.Collections;

namespace PhlegmaticOne.Crypto.Symmetric.Feistel.EncryptionData;

public class FeistelAlgorithmData : IEncryptionData
{
	private readonly IInitialKeyGenerator _initialKeyGenerator;
	private readonly IRoundKeyGenerator _roundKeyGenerator;
	private readonly IFeistelFunction _feistelFunction;
	private readonly IEnumerable<IPostFeistelFunction> _postFeistelFunctions;

	public FeistelAlgorithmData(IInitialKeyGenerator initialKeyGenerator, 
		IRoundKeyGenerator roundKeyGenerator,
		IFeistelFunction feistelFunction,
		IEnumerable<IPostFeistelFunction> postFeistelFunctions,
		int totalRounds = 16, int blockSizeInBits = 64)
	{
		_initialKeyGenerator = initialKeyGenerator;
		_roundKeyGenerator = roundKeyGenerator;
		_feistelFunction = feistelFunction;
		_postFeistelFunctions = postFeistelFunctions;
		TotalRounds = totalRounds;
		BlockSizeInBits = blockSizeInBits;
        _roundKeyGenerator.TotalRounds = totalRounds;
	}

	public int TotalRounds { get; }
	public int BlockSizeInBits { get; }
    public FeistelKey? CurrentFeistelKey { get; private set; }

	public FeistelKey GenerateRoundKey(int round)
	{
		if(round == 0)
		{
			CurrentFeistelKey ??= _initialKeyGenerator.GenerateKey(BlockSizeInBits);
			return CurrentFeistelKey;
		}

		CurrentFeistelKey = _roundKeyGenerator.ChangeKey(CurrentFeistelKey!, round);
        return CurrentFeistelKey;
	}
	public BitArray ApplyFunction(BitArray enterBlock, FeistelKey feistelKey)
	{
		return _feistelFunction.F(enterBlock, feistelKey);
	}
	public BitArray ApplyPostFunctions(BitArray inputBlock)
    {
        return _postFeistelFunctions.Aggregate(inputBlock,
            (current, func) => func.Process(current));
    }
}
