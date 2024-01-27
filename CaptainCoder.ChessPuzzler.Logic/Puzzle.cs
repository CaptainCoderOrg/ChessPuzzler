using System.Diagnostics;

public class Puzzle
{
    public Board Board { get; private set; }
    private Board _start { get; }
    
    public Puzzle(Board board)
    {
        _start = board.Copy();
        Board = _start.Copy();
    }

    public int Pieces()
    {
        int count = 0;
        foreach(int rank in Board.Ranks())
        {
            foreach(char file in Board.Files())
            {
                if (Board.IsOccupied(rank, file))
                {
                    count++;
                }
            }
        }
        return count;
    }

    public bool IsSolved() => Pieces() == 1;

    public void Reset() => Board = _start.Copy();

    public bool Move(Position from, Position to)
    {
        List<Position> validMoves = Moves(from.Rank, from.File);
        if (!validMoves.Contains(to))
        {
            return false;
        }
        ChessPiece piece = Board.GetPiece(from.Rank, from.File);
        Board.RemovePiece(from.Rank, from.File);
        Board.SetPiece(to.Rank, to.File, piece);        
        return true;
    }

    public List<Position> Moves(int rank, char file)
    {
        if (!Board.InBounds(rank, file) || !Board.IsOccupied(rank, file))
        {
            return [];
        }
        List<Position> allMoves = Board.GetPiece(rank, file) switch {
            ChessPiece.Pawn => Pawn(rank, file),
            ChessPiece.Knight => Knight(rank, file),
            ChessPiece.King => King(rank, file),
            ChessPiece.Bishop => Bishop(rank, file),
            ChessPiece.Rook => Rook(rank, file),
            ChessPiece.Queen => Queen(rank, file),
            _ => throw new Exception($"Invalid piece found {Board.GetPiece(rank, file)}"),            
        };
        return allMoves.Where(pos => Board.InBounds(pos.Rank, pos.File))
                       .Where(pos => Board.IsOccupied(pos.Rank, pos.File)).ToList();
    }

    private List<Position> Pawn(int rank, char file)
    {
        return [
            new Position(rank + 1, (char)(file + 1)),
            new Position(rank + 1, (char)(file - 1))
        ];
    }

    private List<Position> Knight(int rank, char file)
    {
        return [
            new Position(rank + 1, (char)(file + 2)),
            new Position(rank + 1, (char)(file - 2)),
            new Position(rank + 2, (char)(file + 1)),
            new Position(rank + 2, (char)(file - 1)),
            new Position(rank - 1, (char)(file + 2)),
            new Position(rank - 1, (char)(file - 2)),
            new Position(rank - 2, (char)(file + 1)),
            new Position(rank - 2, (char)(file - 1)),
        ];
    }

    private List<Position> King(int rank, char file)
    {
        return [
            new Position(rank + 1, (char)(file - 1)),
            new Position(rank + 1, (char)(file + 0)),
            new Position(rank + 1, (char)(file + 1)),

            new Position(rank + 0, (char)(file - 1)),
            new Position(rank + 0, (char)(file + 1)),

            new Position(rank - 1, (char)(file - 1)),
            new Position(rank - 1, (char)(file + 0)),
            new Position(rank - 1, (char)(file + 1)),
        ];
    }

    private List<Position> Rook(int rank, char file)
    {
        return [.. MovesInDir(rank, file, 0, 1),
                .. MovesInDir(rank, file, 0, -1),
                .. MovesInDir(rank, file, 1, 0),
                .. MovesInDir(rank, file, -1, 0),];
    }

    private List<Position> Bishop(int rank, char file)
    {
        return [.. MovesInDir(rank, file, 1, 1),
                .. MovesInDir(rank, file, -1, -1),
                .. MovesInDir(rank, file, -1, 1),
                .. MovesInDir(rank, file, 1, -1),];
    }

    private List<Position> Queen(int rank, char file)
    {
        return [.. Rook(rank, file),
                .. Bishop(rank, file) ];
    }

    private List<Position> MovesInDir(int rank, char file, int dRank, int dFile)
    {
        while (dRank != 0 || dFile != 0)
        {
            rank += dRank;
            file = (char)(file + dFile);
            if (!Board.InBounds(rank, file)) { return []; }
            if (Board.IsOccupied(rank, file)) { return [ new Position(rank, file)]; }
        }
        return [];
    }
}