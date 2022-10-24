using PhlegmaticOne.Crypto.Symmetric.CardanoGrid.Grid;

namespace PhlegmaticOne.Crypto.Symmetric.CardanoGrid.Masks;

public interface IMaskGenerator
{
    Mask GenerateMask(TextGrid textGrid);
}
