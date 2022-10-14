namespace PhlegmaticOne.Crypto.Symmetric.CardanoGrid.Helpers;

public static class MatrixHelpers
{
    public static T[,] Rotate90Clockwise<T>(T[,] array)
    {
        var newItems = (T[,])array.Clone();
        var rank = array.GetLength(0);
        for (var i = 0; i < rank / 2; i++)
        {
            for (var j = i; j < rank - i - 1; j++)
            {
                var temp = newItems[i, j];
                newItems[i, j] = newItems[rank - 1 - j, i];
                newItems[rank - 1 - j, i] = newItems[rank - 1 - i, rank - 1 - j];
                newItems[rank - 1 - i, rank - 1 - j] = newItems[j, rank - 1 - i];
                newItems[j, rank - 1 - i] = temp;
            }
        }
        return newItems;
    }
}
