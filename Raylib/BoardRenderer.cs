using Raylib_cs;

public class BoardRenderer
{
    public const int CellSize = 52;
    public const int PieceSize = 42;
    public const int CenterOffset = (CellSize - PieceSize) / 2;
    public static readonly Color Black = Color.DarkGray;
    public static readonly Color White = Color.Beige;
    public static readonly Color CursorColor = Color.Yellow;
    public static readonly Color SelectedColor = Color.Orange;
    private static Dictionary<ChessPiece, Texture2D> _pieces = null!;
    public BoardRenderer() { LoadTextures(); }

    public int BoardSize(GameState gameState)
    {
        return gameState.CurrentPuzzle.Board.Size * CellSize;
    }
    
    public void DrawBoard(int x, int y, GameState gameState)
    {
        Board board = gameState.CurrentPuzzle.Board;
        foreach (int rank in board.Ranks())
        {
            foreach (char file in board.Files())
            {
                int left = x + (file - 'a') * CellSize;
                int top = y + (board.Size - rank) * CellSize;
                Color tileColor = ColorFromPosition(rank, file, gameState);
                Raylib.DrawRectangle(left, top, CellSize, CellSize, tileColor);
                if (board.IsOccupied(rank, file))
                {
                    DrawPiece(board.GetPiece(rank, file), left + CenterOffset, top + CenterOffset);
                }
            }
        }
    }

    private Color ColorFromPosition(int rank, char file, GameState gameState)
    {
        Position position = new Position(rank, file);
        if (position == gameState.SelectedPosition) { return SelectedColor; }
        if (position == gameState.CursorPosition) { return CursorColor; }
        if ((rank + file) % 2 == 0) { return White; }
        return Black;
    }

    private void DrawPiece(ChessPiece piece, int x, int y) => Raylib.DrawTexture(_pieces[piece], x, y, Color.White);

    private static Dictionary<ChessPiece, Texture2D> LoadTextures()
    {
        if (_pieces == null)
        {
            _pieces = new();
            foreach (ChessPiece piece in Enum.GetValues(typeof(ChessPiece)))
            {
                Image image = Raylib.LoadImage(AssetPath(piece));
                Texture2D texture = Raylib.LoadTextureFromImage(image);
                _pieces[piece] = texture;
                Raylib.UnloadImage(image);
            }
        }
        return _pieces;
    }

    private static string AssetPath(ChessPiece piece)
    {
        return "assets/black/" + piece switch
        {
            ChessPiece.Pawn => "pawn.png",
            ChessPiece.King => "king.png",
            ChessPiece.Queen => "queen.png",
            ChessPiece.Rook => "rook.png",
            ChessPiece.Bishop => "bishop.png",
            ChessPiece.Knight => "knight.png",
            _ => throw new ArgumentException($"Invalid chess piece: {piece}"),
        };
    }
}