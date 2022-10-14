using PhlegmaticOne.Crypto.Symmetric.CardanoGrid.Grid;
using PhlegmaticOne.Crypto.Symmetric.CardanoGrid.Helpers;

namespace PhlegmaticOne.Crypto.Symmetric.CardanoGrid.Masks;

public class Mask
{
	private TextGridItem[,] _grid;

	public Mask(TextGridItem[,] grid)
	{
		_grid = grid;
	}
	public TextGridItem this[int row, int col] => _grid[row, col];
	public void Rotate90Clockwise() => _grid = MatrixHelpers.Rotate90Clockwise(_grid);
}
