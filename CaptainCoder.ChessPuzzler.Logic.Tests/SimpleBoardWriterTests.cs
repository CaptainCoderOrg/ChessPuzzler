using Shouldly;

namespace CaptainCoder.ChessPuzzler.Logic.Tests;

public class SimpleBoardWriterTests
{
    [Fact]
    public void should_write_4x4_grid()
    {
        TextWriter orig = Console.Out;
        TextWriter writer = new StringWriter();
        Console.SetOut(writer);

        Board board = new Board(4);
        board.SetPiece(1, 'a', ChessPiece.Pawn);
        board.SetPiece(2, 'c', ChessPiece.Rook);
        board.SetPiece(4, 'b', ChessPiece.Queen);

        string[] expected = {
            "   a b c d",
            "4   Q     ",
            "3         ",
            "2     R   ",
            "1 P       ",
        };
        SimpleBoardWriter boardWriter = new SimpleBoardWriter();
        boardWriter.Write(board);

        string[]? output = writer.ToString()?.Split(Environment.NewLine);
        output.ShouldNotBeNull();
        output[0].ShouldBe(expected[0]);
        output[1].ShouldBe(expected[1]);
        output[2].ShouldBe(expected[2]);
        output[3].ShouldBe(expected[3]);
        output[4].ShouldBe(expected[4]);
        Console.SetOut(orig);
    }
}