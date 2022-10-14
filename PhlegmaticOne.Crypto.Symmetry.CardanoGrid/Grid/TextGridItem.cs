using System.Diagnostics.CodeAnalysis;

namespace PhlegmaticOne.Crypto.Symmetric.CardanoGrid.Grid;

public struct TextGridItem
{
	public int Row;
	public int Column;
	public int Number;
	public char Letter;
    public static TextGridItem Empty => new()
    {
        Column = 0,
        Number = 0,
        Row = 0,
        Letter = ' '
    };
    public bool IsEmpty() => Column == 0 && Row == 0 && Number == 0 && Letter == '\0';
    public static bool operator ==(TextGridItem a, TextGridItem b) => a.Number == b.Number;
    public static bool operator !=(TextGridItem a, TextGridItem b) => !(a == b);
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if(obj is TextGridItem textGridItem)
        {
            return this == textGridItem;
        }
        return false;
    }
    public override int GetHashCode() => Number;
    public override string ToString() => $"[{Row}, {Column}]: [{Number}, {Letter}]";
}
