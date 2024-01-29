using Shouldly;
using ChessPuzzler;
namespace CaptainCoder.ChessPuzzler.Logic.Tests;

public class BoardTests
{
    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(8)]
    [InlineData(26)]
    public void constructor_should_set_size(int size)
    {
        Board board = new Board(size);
        board.Size.ShouldBe(size);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(27)]
    [InlineData(100)]
    public void constructor_size_must_be_between_2_and_26(int size)
    {
        Assert.Throws<ArgumentException>(() =>
        {
            new Board(size);
        });
    }

    [Theory]
    [InlineData(2, new int[]{1, 2})]
    [InlineData(4, new int[]{1, 2, 3, 4})]
    [InlineData(6, new int[]{1, 2, 3, 4, 5, 6})]
    [InlineData(7, new int[]{1, 2, 3, 4, 5, 6, 7})]
    public void ranks_should_be_1_to_size(int size, int[] expected)
    {
        Board board = new Board(size);
        List<int> ranks = board.Ranks();

        ranks.Count.ShouldBe(size);
        ranks.ShouldBeSubsetOf(expected);
    }

    [Theory]
    [InlineData(2, new char[]{'a', 'b'})]
    [InlineData(4, new char[]{'a', 'b', 'c', 'd'})]
    [InlineData(6, new char[]{'a', 'b', 'c', 'd', 'e', 'f'})]
    [InlineData(7, new char[]{'a', 'b', 'c', 'd', 'e', 'f', 'g'})]
    public void files_should_be_a_to_size(int size, char[] expected)
    {
        Board board = new Board(size);
        List<char> files = board.Files();
        files.Count.ShouldBe(size);
        files.ShouldBeSubsetOf(expected);
    }

    [Theory]
    [InlineData(1, 'a')]
    [InlineData(7, 'b')]
    [InlineData(3, 'd')]
    [InlineData(6, 'f')]
    public void set_piece_should_result_in_is_occupied(int rank, char file)
    {
        Board board = new Board(8);
        board.IsOccupied(rank, file).ShouldBe(false);
        board.SetPiece(rank, file, ChessPiece.Pawn);
        board.IsOccupied(rank, file).ShouldBeTrue();
    }

    [Theory]
    [InlineData(1, 'a', ChessPiece.Pawn)]
    [InlineData(3, 'd', ChessPiece.Rook)]
    [InlineData(7, 'b', ChessPiece.Knight)]
    [InlineData(5, 'e', ChessPiece.Bishop)]
    [InlineData(6, 'f', ChessPiece.Queen)]
    public void set_piece_should_result_in_get_piece_returning_same_piece(int rank, char file, ChessPiece piece)
    {
        Board board = new Board(8);
        board.SetPiece(rank, file, piece);
        board.GetPiece(rank, file).ShouldBe(piece);
    }

    [Theory]
    [InlineData(1, 'a')]
    [InlineData(3, 'd')]
    [InlineData(7, 'b')]
    [InlineData(5, 'e')]
    [InlineData(6, 'f')]
    public void remove_piece_should_result_in_is_not_occupied(int rank, char file)
    {
        Board board = new Board(8);
        board.SetPiece(rank, file, ChessPiece.Pawn);
        board.RemovePiece(rank, file);
        board.IsOccupied(rank, file).ShouldBeFalse();
    }

    [Theory]
    [InlineData(2, 0, 'a')]
    [InlineData(2, 1, 'c')]
    [InlineData(3, 4, 'c')]
    [InlineData(3, 3, 'd')]
    [InlineData(8, 9, 'h')]
    [InlineData(8, 8, 'i')]
    [InlineData(6, 7, 'h')]
    public void cannot_set_piece_outside_of_board(int size, int rank, char file)
    {
        Board board = new Board(size);
        Assert.Throws<ArgumentOutOfRangeException>(() => {
            board.SetPiece(rank, file, ChessPiece.Pawn);
        });
    }

    [Theory]
    [InlineData(2, 0, 'a')]
    [InlineData(2, 1, 'c')]
    [InlineData(3, 4, 'c')]
    [InlineData(3, 3, 'd')]
    [InlineData(8, 9, 'h')]
    [InlineData(8, 8, 'i')]
    [InlineData(6, 7, 'h')]
    public void cannot_get_piece_outside_of_board(int size, int rank, char file)
    {
        Board board = new Board(size);
        Assert.Throws<ArgumentOutOfRangeException>(() => {
            board.GetPiece(rank, file);
        });
    }

    [Theory]
    [InlineData(2, 0, 'a')]
    [InlineData(2, 1, 'c')]
    [InlineData(3, 4, 'c')]
    [InlineData(3, 3, 'd')]
    [InlineData(8, 9, 'h')]
    [InlineData(8, 8, 'i')]
    [InlineData(6, 7, 'h')]
    public void is_occupied_must_be_within_board(int size, int rank, char file)
    {
        Board board = new Board(size);
        Assert.Throws<ArgumentOutOfRangeException>(() => {
            board.IsOccupied(rank, file);
        });
    }

    [Theory]
    [InlineData(1, 'a')]
    [InlineData(7, 'b')]
    [InlineData(3, 'd')]
    [InlineData(6, 'f')]
    public void get_piece_must_be_occupied(int rank, char file)
    {
        Board board = new Board(8);
        Assert.Throws<InvalidOperationException>(() => {
            board.GetPiece(rank, file);
        });
    }

    [Theory]
    [InlineData(1, 'a')]
    [InlineData(7, 'b')]
    [InlineData(3, 'd')]
    [InlineData(6, 'f')]
    public void remove_piece_must_be_occupied(int rank, char file)
    {
        Board board = new Board(8);
        Assert.Throws<InvalidOperationException>(() => {
            board.RemovePiece(rank, file);
        });
    }

}