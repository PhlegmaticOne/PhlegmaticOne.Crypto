using PhlegmaticOne.Crypto.Core.Alphabet;
using PhlegmaticOne.Crypto.Symmetric.CardanoGrid.Helpers;
using PhlegmaticOne.Crypto.Symmetric.CardanoGrid.Masks;
using System.Text;

namespace PhlegmaticOne.Crypto.Symmetric.CardanoGrid.Grid;

public class TextGrid
{
	private readonly TextGridItem[,] _square;
    private readonly ILetterToDigitConverter _alphabet;

    private TextGrid(TextGridItem[,] square, ILetterToDigitConverter alphabet)
    {
        _square = square;
        _alphabet = alphabet;
    }

    public static TextGrid GenerateGrid(ILetterToDigitConverter alphabet, int k)
	{
		var firstQuarter = GenerateFirstQuarter(k);
        var secondQuarter = MatrixHelpers.Rotate90Clockwise(firstQuarter);
        var thirdQuarter = MatrixHelpers.Rotate90Clockwise(secondQuarter);
        var fourthQuarter = MatrixHelpers.Rotate90Clockwise(thirdQuarter);
        var concat = Concat(ref firstQuarter, ref secondQuarter, ref thirdQuarter, ref fourthQuarter, k);
        return new(concat, alphabet);
	}
    public int Rank => _square.GetLength(0);

    public int K => Rank / 2;
    public int KPow2 
    {
        get
        {
            var k = K;
            return k * k;
        } 
    }
    public TextGridItem[] As1DArray()
    {
        var rank = Rank;
        var result = new TextGridItem[rank * rank];
        for (var i = 0; i < rank; i++)
        {
            for (var j = 0; j < rank; j++)
            {
                result[(i * rank) + j] = _square[i, j];
            }
        }
        return result;

    }
    public void ApplyMask(string value, Mask mask)
    {

        if(value.Length < KPow2)
        {
            value = FillString(value);
        }

        var rank = Rank;
        for (var i = 0; i < rank; i++)
        {
            for (var j = 0; j < rank; j++)
            {
                var maskCell = mask[i, j];
                if(maskCell.IsEmpty() == false)
                {
                    _square[i, j].Letter = value[maskCell.Number];
                }
            }
        }
    }

    public string ReadByMask(Mask mask, int countToRead)
    {
        var rank = Rank;
        var read = new List<TextGridItem>();
        for (var i = 0; i < rank; i++)
        {
            for (var j = 0; j < rank; j++)
            {
                var maskCell = mask[i, j];
                if (maskCell.IsEmpty() == false)
                {
                    read.Add(_square[i, j]);
                }
            }
        }
        var ordered = read.OrderBy(x => x.Number).Take(countToRead).Select(x => x.Letter).ToArray();

        return new string(ordered);
    }

    public string FillString(string initial)
    {
        var toAdd = KPow2 - initial.Length;
        var random = Random.Shared;

        var sb = new StringBuilder();
        for (var i = 0; i < toAdd; i++)
        {
            var rnd = random.Next(0, _alphabet.Length);
            sb.Append(_alphabet.ElementAt(rnd).Key);
        }
        return initial + sb;
    }
    public string ToView()
    {
        var rank = Rank;
        var sb = new StringBuilder();
        sb.Append('\n');
        for (var i = 0; i < rank; i++)
        {
            sb.Append("[ ");
            for (var j = 0; j < rank; j++)
            {
                var letter = _square[i, j];
                sb.Append($"{letter.Letter}, ");
            }
            sb.Append("]\n");
        }
        return sb.ToString();
    }
    private static TextGridItem[,] GenerateFirstQuarter(int k)
    {
        var quater = new TextGridItem[k, k];
        for (int i = 0; i < k; i++)
        {
            for (int j = 0; j < k; j++)
            {
                quater[i, j] = new TextGridItem
                {
                    Row = i,
                    Column = j,
                    Letter = ' ',
                    Number = i * k + j
                };
            }
        }
        return quater;
    }
    private static TextGridItem[,] Concat(ref TextGridItem[,] first,
        ref TextGridItem[,] second, 
        ref TextGridItem[,] third, 
        ref TextGridItem[,] fourth,
        int k)
    {
        var result = new TextGridItem[k * 2, k * 2];
        FillQuarter(ref result, ref first, 0, 0, k);
        FillQuarter(ref result, ref second, 0, k, k);
        FillQuarter(ref result, ref third, k, k, k);
        FillQuarter(ref result, ref fourth, k, 0, k);

        return result;
    }
    private static void FillQuarter(ref TextGridItem[,] result, ref TextGridItem[,] quater, int startRow, int startColumn, int k)
    {
        for (var i = startRow; i < k + startRow; i++)
        {
            for (var j = startColumn; j < k + startColumn; j++)
            {
                result[i, j] = quater[i - startRow, j - startColumn];
                result[i, j].Row = i;
                result[i, j].Column = j;
            }
        }
    }
}
