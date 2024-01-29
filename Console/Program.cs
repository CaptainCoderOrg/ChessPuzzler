using System.Text;
using ChessPuzzler;

Console.Clear();
Console.OutputEncoding = Encoding.UTF8;

GameState gameState = new GameState(1);
GameController controller = new GameController(gameState);
GameWriter gameWriter = new GameWriter{Left = 0, Top = 0};

Console.CursorVisible = false;
ConsoleKeyInfo info;
do
{
    gameWriter.Write(gameState);
    info = Console.ReadKey(true);
    controller.HandleInput(info);
} while(info.Key != ConsoleKey.Escape);
