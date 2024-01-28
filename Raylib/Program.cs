using Raylib_cs;

Run();

void Run()
{
    Raylib.InitWindow(800, 480, "Hello World");

    GameState gameState = new GameState(1);
    GameController gameController = new GameController(gameState);
    GameRenderer renderer = new GameRenderer();

    Raylib.SetWindowMonitor(1);
    while (!Raylib.WindowShouldClose())
    {
        gameController.HandleInput();

        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.White);
        renderer.DrawBoard(0, 0, gameState);
        Raylib.EndDrawing();
    }

    Raylib.CloseWindow();
}