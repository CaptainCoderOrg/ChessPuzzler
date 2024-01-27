// See https://aka.ms/new-console-template for more information
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
TextWriter orig = Console.Out;
// TextWriter textWriter = new StringWriter();
// Console.SetOut(textWriter);
Board board = new Board(4);
board.SetPiece(1, 'a', ChessPiece.Pawn);
board.SetPiece(2, 'c', ChessPiece.Rook);
board.SetPiece(4, 'b', ChessPiece.Queen);
SimpleBoardWriter writer = new SimpleBoardWriter();

writer.Write(board);
Console.SetOut(orig);
Console.WriteLine("Done");

// string[] lines = textWriter.ToString()!.Split(Environment.NewLine);
// foreach (string line in lines)
// {
//     Console.WriteLine($"\"{line}\"");
// }