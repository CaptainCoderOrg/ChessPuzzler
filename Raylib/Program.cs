using Raylib_cs;

const int CellSize = 52;
const int PieceSize = 42;
const int CenterOffset = (CellSize - PieceSize) / 2;
Color FaschatGreen = new Color(57, 255, 20, 255);
Color Black = Color.DarkGray;
Color White = Color.Beige;

string[] boardData = File.ReadAllLines("puzzles/01.txt");
// string[] boardData = File.ReadAllLines("5x5.txt");
Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));
Dictionary<ChessPiece, Texture2D> Pieces;

Run();

void Run()
{
    Raylib.InitWindow(800, 480, "Hello World");

    Pieces = LoadTextures();
    Raylib.SetWindowMonitor(1);
    while (!Raylib.WindowShouldClose())
    {
        HandleInput();
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.White);
        DrawBoard(0, 0, puzzle.Board);
        Raylib.EndDrawing();
    }

    Raylib.CloseWindow();
}

void HandleInput()
{
    int key;
    while ((key = Raylib.GetKeyPressed()) != 0)
    {
        Action action = (KeyboardKey)key switch
        {
            KeyboardKey.W => () => { Console.WriteLine("W"); },
            _ => () => {}
        };
        action.Invoke();
    }
}

void DrawBoard(int x, int y, Board board)
{
    foreach (int rank in board.Ranks())
    {
        foreach (char file in board.Files())
        {
            Color tileColor = ColorFromPosition(rank, file, board);
            int left = x + (file - 'a') * CellSize;
            int top = y + (board.Size - rank) * CellSize;
            Raylib.DrawRectangle(left, top, CellSize, CellSize, tileColor);
            if (board.IsOccupied(rank, file))
            {
                DrawPiece(board.GetPiece(rank, file), left + CenterOffset, top + CenterOffset);
            }
            Raylib.DrawText($"{file}{rank}", left, top, 12, Color.Black);
        }
    }
}

Color ColorFromPosition(int rank, char file, Board board)
{
    int cellId = rank + file;
    if (cellId % 2 == 0)
    {
        return White;
    }
    return Black;
}

void DrawPiece(ChessPiece piece, int x, int y)
{
    Raylib.DrawTexture(Pieces[piece], x, y, Color.White);
}

Dictionary<ChessPiece, Texture2D> LoadTextures()
{
    Dictionary<ChessPiece, Texture2D> pieces = new();
    foreach (ChessPiece piece in Enum.GetValues(typeof(ChessPiece)))
    {
        Image image = Raylib.LoadImage(AssetPath(piece));
        Texture2D texture = Raylib.LoadTextureFromImage(image);
        pieces[piece] = texture;
        Raylib.UnloadImage(image);
    }
    return pieces;
}

string AssetPath(ChessPiece piece)
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

