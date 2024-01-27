// See https://aka.ms/new-console-template for more information
using System.Text;

Console.Clear();
Console.OutputEncoding = Encoding.UTF8;

GameState gameState = new GameState(1);

ConsoleKeyInfo info;
do
{
    gameState.WriteBoard(1, 1);
    info = Console.ReadKey();
    gameState.HandleInput(info);
} while(info.Key != ConsoleKey.Escape);

// string[] lines = textWriter.ToString()!.Split(Environment.NewLine);
// foreach (string line in lines)
// {
//     Console.WriteLine($"\"{line}\"");
// }