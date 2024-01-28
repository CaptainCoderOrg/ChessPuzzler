using System.Numerics;
using Raylib_cs;

public class InfoRenderer
{
    public static readonly Color DefaultColor = Color.White;
    private Font _font;
    public const float FontSize = 24;
    public const float LineHeight = 24;
    public const float Spacing = 2;
    private float _top;
    private float _left;
    private float _leftReturn;
    private float _right;
    private float _startTop;

    public InfoRenderer()
    {
        _font = Raylib.LoadFont("assets/fonts/romulus.png");
    }

    private void DrawTitle(int x, int y, GameState gameState)
    {
        Write($"Puzzle #{gameState.PuzzleId} - ");
        if (gameState.CheatedPuzzles.ContainsKey(gameState.PuzzleId))
        {
            WriteLine("Cheated", Color.Yellow);
        }
        else if (gameState.SolvedPuzzles.Contains(gameState.PuzzleId))
        {
            WriteLine("Solved", Color.Green);
        }
        else { 
            WriteLine("Unsolved", Color.Red); 
        }
    }

    private void DrawText(string text, float left, float top, float fontsize, Color color)
    {
        Raylib.DrawTextEx(_font, text, new Vector2(left + 1, top + 1), fontsize, Spacing, Color.Black);
        Raylib.DrawTextEx(_font, text, new Vector2(left, top), fontsize, Spacing, color);
        
    }

    private void Write(string text) => Write(text, DefaultColor);
    private void WriteLine(string text) => WriteLine(text, DefaultColor);
    private void Write(string text, Color color)
    {
        DrawText(text, _left, _top, FontSize, color);
        var textWidth = Raylib.MeasureTextEx(_font, text, FontSize, Spacing);
        _left += textWidth.X;
        _right = Math.Max(_left, _right);
    }

    private void WriteLine(string text, Color color)
    {
        DrawText(text, _left, _top, FontSize, color);
        var textWidth = Raylib.MeasureTextEx(_font, text, FontSize, Spacing);
        _top += LineHeight;
        _right = Math.Max(_left + textWidth.X, _right);
        _left = _leftReturn;
    }

    public Vector2 RenderInfo(int x, int y, GameState gameState)
    {
        _startTop = y;
        _leftReturn = x;
        _top = y;
        _right = x;
        _left = x;
        DrawTitle(x, y, gameState);
        WriteLine("W/A/S/D - Move Cursor");
        WriteLine("Space - Select/Move");
        WriteLine("N - Next Puzzle");
        WriteLine("P - Previous Puzzle");
        WriteLine("R - Reset Puzzle");
        WriteLine("Y - Cheat");
        WriteLine("ESC - Quit");
        if (gameState.CheatedPuzzles.TryGetValue(gameState.PuzzleId, out SolverResult? solution))
        {
            Write($"Difficulty: {solution.Attempts} | ");
            foreach ((Position from, Position to) in solution.Moves)
            {
                Write($"{from.File}{from.Rank}-{to.File}{to.Rank} ");
            }
            WriteLine(string.Empty);
        }
        
        return new Vector2(_right - _leftReturn, _top - _startTop);
    }

}
