namespace PhlegmaticOne.Crypto.PolybiusSquare.Alphabet;

public class SquareAlphabet
{
    private readonly char[,] _alphabet;

    private SquareAlphabet(char[,] alphabet) => _alphabet = alphabet;
    public static SquareAlphabet FromAlphabet(string alphabet)
    {
        var alphabetRank = (int)Math.Ceiling(Math.Sqrt(alphabet.Length));
        var matrix = new char[alphabetRank, alphabetRank];
        var fillNum = 0;
        for (int i = 0; i < alphabetRank; i++)
        {
            for (int j = 0; j < alphabetRank; j++)
            {
                var index = i * alphabetRank + j;
                matrix[i, j] = index < alphabet.Length ? alphabet[index] : (char)(fillNum++);
            }
        }
        return new(matrix);
    }
    public int Rank => _alphabet.GetLength(0);
    public char this[int row, int col] => _alphabet[row, col];
    public (int, int) this[char letter]
    {
        get
        {
            for (int i = 0; i < _alphabet.GetLength(0); i++)
            {
                for (int j = 0; j < _alphabet.GetLength(1); j++)
                {
                    if(_alphabet[i, j] == letter)
                    {
                        return (i, j);
                    }   
                }
            }
            return (-1, -1);
        }
    }
}
