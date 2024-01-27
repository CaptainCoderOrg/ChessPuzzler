public static class BoardReader
{
    public static Board Read(string[] ranks)
    {
        int rank = ranks.Length;
        Board b = new Board(ranks.Length);
        foreach (string row in ranks)
        {
            if (row.Length != ranks.Length) { throw new ArgumentException($"Not a valid board. Must have equal dimensions but found row with length {row.Length}"); }
            char file = (char)('a'-1);
            foreach (char ch in row)
            {
                file++;
                if (ch == '.') { continue; }
                b.SetPiece(rank, file, FromChar(ch));
            }
            rank--;
        }
        return b;
    }

    private static ChessPiece FromChar(char ch)
    {
        return ch switch 
        {
            'K' => ChessPiece.King,
            'Q' => ChessPiece.Queen,
            'R' => ChessPiece.Rook,
            'B' => ChessPiece.Bishop,
            'N' => ChessPiece.Knight,
            'P'=> ChessPiece.Pawn,
            _ => throw new Exception($"Could not convert '{ch}' to ChessPiece."),
        };
    }
}