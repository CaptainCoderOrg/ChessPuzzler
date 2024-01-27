public class GameController
{
    public HashSet<int> SolvedPuzzles = new();
    public Dictionary<int, SolverResult> CheatedPuzzles = new();
    const int MaxPuzzleId = 20;
    public Puzzle CurrentPuzzle { get; set; }
    public int PuzzleId { get; private set; }
    private Position _cursor;
    public Position? SelectedPosition { get; private set; }
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

    public GameController(int id)
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
        if (userInput.KeyChar == '!')
        {
            Cheat();
        }
        Action handler = userInput.Key switch
        {
            ConsoleKey.R => CurrentPuzzle.Reset,
            ConsoleKey.N => () => SwitchPuzzle(1),
            ConsoleKey.P => () => SwitchPuzzle(-1),
            _ => () => { },
        };
        handler.Invoke();
    }

    private void Cheat()
    {
        DumbSolver solver = new DumbSolver();
        SolverResult result = solver.Solve(CurrentPuzzle);
        CurrentPuzzle.Reset();  
        CheatedPuzzles[PuzzleId] = result; 
    }

    private void SwitchPuzzle(int dir)
    {
        PuzzleId = Math.Clamp(PuzzleId + dir, 1, MaxPuzzleId);
        CurrentPuzzle = LoadPuzzle(PuzzleId);
    }

    private void HandleMove(ConsoleKeyInfo userInput)
    {
        if (userInput.Key != ConsoleKey.Spacebar) { return; }
        if (SelectedPosition.HasValue)
        {
            CurrentPuzzle.Move(SelectedPosition.Value, CursorPosition);
            SelectedPosition = null;
            if (CurrentPuzzle.IsSolved())
            {
                SolvedPuzzles.Add(PuzzleId);
            }
        }
        else
        {
            SelectedPosition = CursorPosition;
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
}