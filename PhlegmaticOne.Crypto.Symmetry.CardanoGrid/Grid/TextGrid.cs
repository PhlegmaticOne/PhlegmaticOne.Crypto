namespace PhlegmaticOne.Crypto.Symmetry.CardanoGrid.Grid;

public class TextGrid
{
    public IEnumerable<int> IndexesToSelect { get; }
	public TextGrid(IEnumerable<int> indexesToSelect)
	{
		IndexesToSelect = indexesToSelect;
	}
}
