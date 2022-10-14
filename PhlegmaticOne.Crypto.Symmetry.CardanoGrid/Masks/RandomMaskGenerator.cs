using PhlegmaticOne.Crypto.Symmetric.CardanoGrid.Grid;

namespace PhlegmaticOne.Crypto.Symmetric.CardanoGrid.Masks;

public class RandomMaskGenerator : IMaskGenerator
{
    private readonly Random _random = Random.Shared;
    public Mask GenerateMask(TextGrid textGrid)
    {
        var result = new TextGridItem[textGrid.Rank, textGrid.Rank];
        var arr = textGrid.As1DArray();
        for (var i = 0; i < textGrid.KPow2; i++)
        {
            var allByNumber = arr.Where(x => x.Number == i).ToList();
            var rnd = _random.Next(0, allByNumber.Count);
            var rndCell = allByNumber[rnd];
            result[rndCell.Row, rndCell.Column] = rndCell;
        }
        return new Mask(result);
    }
}
