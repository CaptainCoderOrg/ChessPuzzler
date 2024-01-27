using Shouldly;

namespace CaptainCoder.ChessPuzzler.Logic.Tests;

public class PuzzleTests
{
    [Fact]
    public void pieces_should_count_pieces_1()
    {
        string[] boardData = [
        //   abcd
            "....", // 4
            "....", // 3
            "....", // 2
            "P...", // 1
        ];

        Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));
        puzzle.Pieces().ShouldBe(1);
    }

    [Fact]
    public void pieces_should_count_pieces_2()
    {
        string[] boardData = [
        //   abcd
            "....", // 4
            "....", // 3
            ".R..", // 2
            "P...", // 1
        ];
        Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));
        puzzle.Pieces().ShouldBe(2);
    }

    [Fact]
    public void pieces_should_count_pieces_3()
    {
        string[] boardData = [
        //   abcd
            "....", // 4
            "..B.", // 3
            ".R..", // 2
            "P...", // 1
        ];
        Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));
        puzzle.Pieces().ShouldBe(3);
    }

    [Fact]
    public void pieces_should_count_pieces_4()
    {
        string[] boardData = [
        //   abcd
            ".N..", // 4
            "..B.", // 3
            ".R..", // 2
            "P...", // 1
        ];
        Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));
        puzzle.Pieces().ShouldBe(4);
    }

    [Fact]
    public void is_solved_when_one_piece_remains()
    {
        Board board = new Board(4);
        board.SetPiece(1, 'a', ChessPiece.Pawn);

        Puzzle puzzle = new Puzzle(board);
        puzzle.IsSolved().ShouldBeTrue();
    }

    [Fact]
    public void is_not_solved_when_two_pieces_remains()
    {
        Board board = new Board(4);
        board.SetPiece(1, 'a', ChessPiece.Pawn);
        board.SetPiece(2, 'b', ChessPiece.Rook);

        Puzzle puzzle = new Puzzle(board);
        puzzle.IsSolved().ShouldBeFalse();
    }

    [Theory]
    [InlineData(2, 'b', 1, 'a')]
    [InlineData(2, 'a', 1, 'a')]
    [InlineData(2, 'a', 3, 'a')]
    [InlineData(4, 'd', 1, 'a')]
    public void move_should_return_false_when_move_is_invalid(int fromRank, char fromFile, int toRank, char toFile)
    {
        string[] boardData = [
        //   abcd
            ".N..", // 4
            "..B.", // 3
            ".R..", // 2
            "P...", // 1
        ];
        Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));
        puzzle.Move(new Position(fromRank, fromFile), new Position(toRank, toFile)).ShouldBeFalse();
    }

    [Theory]
    [InlineData(1, 'a', 2, 'b')]
    [InlineData(2, 'b', 4, 'b')]
    [InlineData(3, 'c', 2, 'b')]
    [InlineData(3, 'c', 4, 'b')]
    public void move_should_return_true_when_move_is_valid(int fromRank, char fromFile, int toRank, char toFile)
    {
        string[] boardData = [
        //   abcd
            ".N..", // 4
            "..B.", // 3
            ".R..", // 2
            "P...", // 1
        ];
        Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));
        puzzle.Move(new Position(fromRank, fromFile), new Position(toRank, toFile)).ShouldBeTrue();
    }

    [Theory]
    [InlineData(1, 'a', 2, 'b')]
    [InlineData(2, 'b', 4, 'b')]
    [InlineData(3, 'c', 2, 'b')]
    [InlineData(3, 'c', 4, 'b')]
    public void valid_move_should_remove_piece_and_set_piece(int fromRank, char fromFile, int toRank, char toFile)
    {
        string[] boardData = [
        //   abcd
            ".N..", // 4
            "..B.", // 3
            ".R..", // 2
            "P...", // 1
        ];
        Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));

        // This is the piece we are trying to move
        ChessPiece moving = puzzle.Board.GetPiece(fromRank, fromFile);
        puzzle.Move(new Position(fromRank, fromFile), new Position(toRank, toFile));
        
        // There should be no piece at the position that we moved from
        puzzle.Board.IsOccupied(fromRank, fromFile).ShouldBeFalse();

        // There should be a piece at the position that we moved to
        puzzle.Board.IsOccupied(toRank, toFile);

        // The piece should be the one that was at the original position
        puzzle.Board.GetPiece(toRank, toFile).ShouldBe(moving);
    }

    [Fact]
    public void test_simple_puzzle()
    {
        string[] boardData = [
        //   abcd
            ".N..", // 4
            "..B.", // 3
            ".R..", // 2
            "P...", // 1
        ];
        Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));
        // Pawn takes rook
        puzzle.Board.GetPiece(2, 'b').ShouldBe(ChessPiece.Rook);
        puzzle.Move(new Position(1, 'a'), new Position(2, 'b')).ShouldBeTrue();
        puzzle.Board.GetPiece(2, 'b').ShouldBe(ChessPiece.Pawn);
        // Pawn takes bishop
        puzzle.Board.GetPiece(3, 'c').ShouldBe(ChessPiece.Bishop);
        puzzle.Move(new Position(2, 'b'), new Position(3, 'c')).ShouldBeTrue();
        puzzle.Board.GetPiece(3, 'c').ShouldBe(ChessPiece.Pawn);
        // Pawn takes knight
        puzzle.Board.GetPiece(4, 'b').ShouldBe(ChessPiece.Knight);
        puzzle.Move(new Position(3, 'c'), new Position(4, 'b')).ShouldBeTrue();
        puzzle.Board.GetPiece(4, 'b').ShouldBe(ChessPiece.Pawn);

        puzzle.Pieces().ShouldBe(1);
        puzzle.IsSolved().ShouldBeTrue();

        puzzle.Reset();
        puzzle.Pieces().ShouldBe(4);
        puzzle.Board.IsOccupied(1, 'a').ShouldBeTrue();
        puzzle.Board.GetPiece(1, 'a').ShouldBe(ChessPiece.Pawn);
        puzzle.Board.IsOccupied(2, 'b').ShouldBeTrue();
        puzzle.Board.GetPiece(2, 'b').ShouldBe(ChessPiece.Rook);
        puzzle.Board.IsOccupied(3, 'c').ShouldBeTrue();
        puzzle.Board.GetPiece(3, 'c').ShouldBe(ChessPiece.Bishop);
        puzzle.Board.IsOccupied(4, 'b').ShouldBeTrue();
        puzzle.Board.GetPiece(4, 'b').ShouldBe(ChessPiece.Knight);
    }
}