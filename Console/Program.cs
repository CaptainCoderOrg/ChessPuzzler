// See https://aka.ms/new-console-template for more information
using System.Text;

Console.Clear();
Console.OutputEncoding = Encoding.UTF8;

GameController gameState = new GameController(1);
GameWriter gameWriter = new GameWriter{Left = 0, Top = 0};

Console.CursorVisible = false;
ConsoleKeyInfo info;
do
{
    gameWriter.Write(gameState);
    info = Console.ReadKey(true);
    gameState.HandleInput(info);
} while(info.Key != ConsoleKey.Escape);

// string[] lines = textWriter.ToString()!.Split(Environment.NewLine);
// foreach (string line in lines)
// {
//     Console.WriteLine($"\"{line}\"");
// }