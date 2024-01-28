public class GameController
{
    private GameState _gameState;
    public GameController(GameState state)
    {
        _gameState = state;
    }
    public void HandleInput(ConsoleKeyInfo userInput)
    {
        UpdateCursor(userInput);
        if (userInput.KeyChar == '!')
        {
            _gameState.Cheat();
        }
        Action handler = userInput.Key switch
        {
            ConsoleKey.R => _gameState.CurrentPuzzle.Reset,
            ConsoleKey.N => () => _gameState.NextPuzzle(1),
            ConsoleKey.P => () => _gameState.NextPuzzle(-1),
            ConsoleKey.Spacebar => () => _gameState.HandleSelect(),
            _ => () => { }
            ,
        };
        handler.Invoke();
    }

    private void UpdateCursor(ConsoleKeyInfo userInput)
    {
        (int rank, char file) = _gameState.CursorPosition;
        (int newRank, int newFile) = userInput.Key switch
        {
            ConsoleKey.D => (rank, file + 1),
            ConsoleKey.A => (rank, file - 1),
            ConsoleKey.W => (rank + 1, file),
            ConsoleKey.S => (rank - 1, file),
            _ => (rank, file),
        };
        _gameState.CursorPosition = new Position(newRank, (char)newFile);
    }



}