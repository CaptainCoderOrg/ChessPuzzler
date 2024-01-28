using System.Numerics;
using Raylib_cs;

Run();

void Run()
{
    
    Raylib.InitWindow(800, 280, "Chess Puzzler with Raylib-cs");

    GameState gameState = new GameState(1);
    GameController gameController = new GameController(gameState);
    BoardRenderer boardRenderer = new BoardRenderer();
    InfoRenderer infoRenderer = new InfoRenderer();
    int spacing = 10;

    Raylib.SetWindowMonitor(1);
    while (!Raylib.WindowShouldClose())
    {
        gameController.HandleInput();
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.DarkBlue);
        boardRenderer.DrawBoard(10, 10, gameState);
        Vector2 textSize = infoRenderer.RenderInfo(spacing + BoardRenderer.CellSize*gameState.CurrentPuzzle.Board.Size + spacing, spacing, gameState);
        int boardSize = boardRenderer.BoardSize(gameState);
        int width = boardSize + (int)Math.Ceiling(textSize.X) + spacing * 3;
        int height = Math.Max(boardSize, (int)Math.Ceiling(textSize.Y)) + spacing * 2;
        Raylib.SetWindowSize(width, height);
        Raylib.EndDrawing();
    }

    Raylib.CloseWindow();
}