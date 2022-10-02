using PhlegmaticOne.Crypto.Core.Base;
using PhlegmaticOne.Crypto.Symmetry.CardanoGrid.Grid;

namespace PhlegmaticOne.Crypto.Symmetry.CardanoGrid.EncryptionData;

public class CardanoGridAlgorithmEncryptionData : IEncryptionData
{
    public TextGrid Stencil { get; set; }
	public CardanoGridAlgorithmEncryptionData(TextGrid stencil)
	{
		Stencil = stencil;
	}
}
