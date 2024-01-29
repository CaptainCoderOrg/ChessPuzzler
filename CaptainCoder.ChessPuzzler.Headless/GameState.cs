namespace ChessPuzzler;
public class GameState
{
    const int MaxPuzzleId = 80;
    public HashSet<int> SolvedPuzzles = new();
    public Dictionary<int, SolverResult> CheatedPuzzles = new();
    public Puzzle CurrentPuzzle { get; private set; }
    public int PuzzleId { get; private set; }
    public IPuzzleLibrary PuzzleLibrary { get; private set; }
    public Position? SelectedPosition { get; private set; }
    private Position _cursor;
    public Position CursorPosition
    {
        get => _cursor;
        set
        {
            int size = CurrentPuzzle.Board.Size;
            var (rank, file) = value;
            rank = Math.Clamp(rank, 1, size);
            file = (char)(Math.Clamp(file - 'a', 0, size - 1) + 'a');
            _cursor = new Position(rank, file);
        }
    }

    public GameState(int id, IPuzzleLibrary? puzzleLibrary = null)
    {
        PuzzleLibrary = puzzleLibrary ?? new FileSystemPuzzleLibrary("puzzles/");
        PuzzleId = id;
        CurrentPuzzle = PuzzleLibrary.LoadPuzzle(id);
        CursorPosition = new Position(CurrentPuzzle.Board.Size, 'a');
    }

    public void Cheat()
    {
        MonteCarloSolver solver = new MonteCarloSolver();
        SolverResult result;
        List<(Position, Position)> moves = [];
        int difficulty = 0;
        int trials = 1;
        for (int trial = 0; trial < trials; trial++)
        {
            
            result = solver.Solve(CurrentPuzzle);
            difficulty += result.Attempts;
            moves = result.Moves;
        }
        CheatedPuzzles[PuzzleId] = new SolverResult(difficulty/trials, moves); 
        CurrentPuzzle.Reset();  
    }

    public void NextPuzzle(int dir)
    {
        PuzzleId = Math.Clamp(PuzzleId + dir, 1, MaxPuzzleId);
        CurrentPuzzle = PuzzleLibrary.LoadPuzzle(PuzzleId);
    }

    public void HandleSelect()
    {
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

}