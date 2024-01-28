// See https://aka.ms/new-console-template for more information
using System.Text;

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

// string[] lines = textWriter.ToString()!.Split(Environment.NewLine);
// foreach (string line in lines)
// {
//     Console.WriteLine($"\"{line}\"");
// }