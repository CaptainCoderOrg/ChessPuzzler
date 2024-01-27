public class GameState
{
    const int MaxPuzzleId = 20;
    public Puzzle CurrentPuzzle { get; set; }
    public int PuzzleId { get; private set; }
    private Position _cursor;
    private Position? _selectedPosition;
    public Position CursorPosition
    {
        get => _cursor;
        private set
        {
            int size = CurrentPuzzle.Board.Size;
            var (rank, file) = value;
            rank = Math.Clamp(rank, 1, size);
            file = (char)(Math.Clamp(file - 'a', 0, size - 1) + 'a');
            _cursor = new Position(rank, file);
        }
    }

    public GameState(int id)
    {
        PuzzleId = id;
        CurrentPuzzle = LoadPuzzle(id);
        CursorPosition = new Position(CurrentPuzzle.Board.Size, 'a');
    }

    private static Puzzle LoadPuzzle(int id) => LoadPuzzle($"puzzles/{id.ToString().PadLeft(2, '0')}.txt");

    private static Puzzle LoadPuzzle(string filename)
    {
        string[] boardData = File.ReadAllLines(filename);
        return new Puzzle(BoardReader.Read(boardData));
    }


    public void HandleInput(ConsoleKeyInfo userInput)
    {
        UpdateCursor(userInput);
        HandleMove(userInput);
        if (userInput.Key == ConsoleKey.R)
        {
            CurrentPuzzle.Reset();
        }

        // P -- previous puzzle
        // N -- next puzzle
        if (userInput.Key == ConsoleKey.P)
        {
            NextPuzzle(-1);
        }
        if (userInput.Key == ConsoleKey.N)
        {
            NextPuzzle(1);
        }
    }
    private void NextPuzzle(int dir)
    {
        PuzzleId = Math.Clamp(PuzzleId + dir, 1, MaxPuzzleId);
        CurrentPuzzle = LoadPuzzle(PuzzleId);
    }

    private void HandleMove(ConsoleKeyInfo userInput)
    {
        if (userInput.Key != ConsoleKey.Spacebar) { return; }
        if (_selectedPosition.HasValue)
        {
            CurrentPuzzle.Move(_selectedPosition.Value, CursorPosition);
            _selectedPosition = null;
        }
        else
        {
            _selectedPosition = CursorPosition;
        }
    }

    private void UpdateCursor(ConsoleKeyInfo userInput)
    {
        (int rank, char file) = CursorPosition;
        (int newRank, int newFile) = userInput.Key switch
        {
            ConsoleKey.D => (rank, file + 1),
            ConsoleKey.A => (rank, file - 1),
            ConsoleKey.W => (rank + 1, file),
            ConsoleKey.S => (rank - 1, file),
            _ => (rank, file),
        };
        CursorPosition = new Position(newRank, (char)newFile);
    }

    public void WriteBoard(int left, int top)
    {
        var board = CurrentPuzzle.Board;
        Console.SetCursorPosition(left, top++);
        bool isBlack = true;
        Console.WriteLine($"   {string.Join(" ", board.Files())}");
        Console.SetCursorPosition(left, top++);
        foreach (int rank in board.Ranks())
        {
            Console.Write($"{rank} ");
            if (board.Size % 2 == 0) { isBlack = !isBlack; }
            foreach (char file in board.Files())
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = isBlack ? ConsoleColor.DarkGray : ConsoleColor.White;
                if (_selectedPosition.HasValue && _selectedPosition == new Position(rank, file))
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                }
                else if (new Position(rank, file) == CursorPosition)
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                }
                if (board.IsOccupied(rank, file))
                {
                    ChessPiece piece = board.GetPiece(rank, file);
                    Console.Write($"{(char)piece} ");
                }
                else
                {
                    Console.Write("  ");
                }
                isBlack = !isBlack;
            }
            Console.ResetColor();
            Console.SetCursorPosition(left, top++);
        }
    }
}