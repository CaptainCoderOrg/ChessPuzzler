public class DumbSolver
{
    public SolverResult Solve(Puzzle puzzle, int maxAttempts = 100000)
    {
        puzzle.Reset();
        int attempts = 0;
        while (attempts++ < maxAttempts)
        {
            List<(Position, Position)> moves = new();
            do
            {
                var possibleMoves = AllMoves(puzzle);
                if (possibleMoves.Length == 0) { break; }
                Random.Shared.Shuffle(possibleMoves);
                var move = possibleMoves[0];
                moves.Add(move);
                puzzle.Move(move.From, move.To);

            } while (!puzzle.IsSolved());

            // If we solved it, return the solution
            if (puzzle.IsSolved())
            {
                return new SolverResult(attempts, moves);
            }
            // Otherwise reset and try again
            puzzle.Reset();
        }
        // Could not solve
        return new SolverResult(-1, []);
    }

    public Span<(Position From, Position To)> AllMoves(Puzzle puzzle)
    {
        List<(Position, Position)> moves = [];
        foreach (int rank in puzzle.Board.Ranks())
        {
            foreach (char file in puzzle.Board.Files())
            {
                Position from = new Position(rank, file);
                moves.AddRange(puzzle.Moves(rank, file).Select(to => (from, to)));
            }
        }
        return moves.ToArray();
    }
}

public record SolverResult(int Difficulty, List<(Position, Position)> Moves);