using PhlegmaticOne.Crypto.Core.Alphabet;
using PhlegmaticOne.Crypto.Core.Base;
using PhlegmaticOne.Crypto.Symmetric.CardanoGrid.Grid;
using PhlegmaticOne.Crypto.Symmetric.CardanoGrid.Masks;

namespace PhlegmaticOne.Crypto.Symmetric.CardanoGrid.EncryptionData;

public class CardanoGridAlgorithmEncryptionData : IEncryptionData
{
	private readonly IMaskGenerator _maskGenerator;
	private readonly ILetterToDigitConverter _alphabet;

	public TextGrid TextGrid { get; set; }
	public Mask Mask { get; private set; }
	public CardanoGridAlgorithmEncryptionData(IMaskGenerator maskGenerator, ILetterToDigitConverter alphabet)
	{
		_maskGenerator = maskGenerator;
		_alphabet = alphabet;
	}
	public TextGrid GenerateTextGrid(string value)
	{
		var s = (double)value.Length / 4;
		var r = Math.Sqrt(s);
		var c = Math.Ceiling(r);
		var k = (int)c;
		TextGrid = TextGrid.GenerateGrid(_alphabet, k);
		return TextGrid;
	}
	public Mask GenerateMask()
	{
		Mask = _maskGenerator.GenerateMask(TextGrid);
		return Mask;
	}
}
