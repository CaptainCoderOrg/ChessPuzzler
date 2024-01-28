using Raylib_cs;

public class GameController
{
    private GameState _gameState;

    public GameController(GameState state)
    {
        _gameState = state;
    }

    public void HandleInput()
    {
        int key;
        while ((key = Raylib.GetKeyPressed()) != 0)
        {
            Action action = (KeyboardKey)key switch
            {
                KeyboardKey.W => () => { MoveCursor(1, 0); },
                KeyboardKey.S => () => { MoveCursor(-1, 0); },
                KeyboardKey.A => () => { MoveCursor(0, -1); },
                KeyboardKey.D => () => { MoveCursor(0, 1); },
                KeyboardKey.Space => _gameState.HandleSelect,
                KeyboardKey.N => () => _gameState.NextPuzzle(1),
                KeyboardKey.P => () => _gameState.NextPuzzle(-1),
                KeyboardKey.R => _gameState.CurrentPuzzle.Reset,
                KeyboardKey.Y => _gameState.Cheat,
                _ => () => { }
            };
            action.Invoke();
        }
    }

void MoveCursor(int dRank, int dFile)
{
    (int rank, char file) = _gameState.CursorPosition;
    _gameState.CursorPosition = new Position(rank + dRank, (char)(file + dFile));
}

}