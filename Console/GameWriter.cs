public class GameWriter
{
    private int _width = 0;
    private int _height = 0;
    public int Left { get; init; }
    public int Top { get; init; }
    private int _top;
    private int _left;

    public void Write(GameController controller)
    {
        if (_height != Console.WindowHeight || _width != Console.WindowWidth )
        {
            Console.Clear();
            _width = Console.WindowWidth;
            _height = Console.WindowHeight;
        }
        WriteInfo(controller);
        WriteBoard(controller);
        
    }

    private void WriteInfo(GameController controller)
    {
        _top = Top;
        _left = Left + (controller.CurrentPuzzle.Board.Size*2) + 4;
        Console.SetCursorPosition(_left, _top);
        Console.Write($"Puzzle #{controller.PuzzleId} - ");
        if (controller.SolvedPuzzles.Contains(controller.PuzzleId))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Solved  ");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Unsolved");
        }
        Console.ResetColor();
        _top++;
        WriteLine("W/A/S/D - Move Cursor");
        WriteLine("Space - Select/Move");
        WriteLine("N - Next Puzzle");
        WriteLine("P - Previous Puzzle");
        WriteLine("R - Reset Puzzle");
        WriteLine("ESC - Quit");

    }

    private void Write(string text)
    {
        Console.SetCursorPosition(_left, _top);
        Console.Write(text);
        _left = Console.CursorLeft;
    }

    private void WriteLine(string line)
    {
        Console.SetCursorPosition(_left, _top++);
        Console.Write(line);
    }

    private void WriteBoard(GameController controller)
    {
        _left = Left;
        _top = Top;
        var board = controller.CurrentPuzzle.Board;
        WriteLine($"   {string.Join(" ", board.Files())}");
        Console.SetCursorPosition(_left, _top++);
        foreach (int rank in board.Ranks())
        {
            Console.Write($"{rank} ");
            foreach (char file in board.Files())
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = TileColor(new Position(rank, file), controller);
                if (board.IsOccupied(rank, file))
                {
                    ChessPiece piece = board.GetPiece(rank, file);
                    Console.Write($"{(char)piece} ");
                }
                else
                {
                    Console.Write("  ");
                }
            }
            Console.ResetColor();
            Console.SetCursorPosition(_left, _top++);
        }
    }

    private ConsoleColor TileColor(Position position, GameController controller)
    {
        if (controller.SelectedPosition.HasValue && controller.SelectedPosition == position)
        {
            return ConsoleColor.DarkYellow;
        }
        
        if (position == controller.CursorPosition)
        {
            return ConsoleColor.Yellow;
        }
        
        if ((position.Rank + position.File) % 2 == 0)
        {
            return ConsoleColor.DarkGray;
        }
        return ConsoleColor.White;
    }
}