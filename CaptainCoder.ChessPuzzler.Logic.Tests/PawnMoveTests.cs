using Shouldly;

namespace CaptainCoder.ChessPuzzler.Logic.Tests;

public class PawnMoveTests
{

    [Fact]
    public void pawn_cannot_move_backwards()
    {
        string[] boardData = [
        //   abcd
            "....", // 4
            "....", // 3
            ".P..", // 2
            "N.B.", // 1
        ];
        Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));
        puzzle.Moves(2, 'b').ShouldBeEmpty();
    }

    [Fact]
    public void pawn_can_move_forward_right()
    {
        string[] boardData = [
        //   abcd
            "....", // 4
            "..B.", // 3
            ".P..", // 2
            "....", // 1
        ];
        Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));
        List<Position> positions = puzzle.Moves(2, 'b');
        positions.Count.ShouldBe(1);
        positions.ShouldContain(new Position(3, 'c'));
    }

    [Fact]
    public void pawn_can_move_forward_left()
    {
        string[] boardData = [
        //   abcd
            "....", // 4
            "R...", // 3
            ".P..", // 2
            "....", // 1
        ];
        Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));
        List<Position> positions = puzzle.Moves(2, 'b');
        positions.Count.ShouldBe(1);
        positions.ShouldContain(new Position(3, 'a'));
    }

    [Fact]
    public void pawn_can_move_forward_left_and_right()
    {
        string[] boardData = [
        //   abcd
            "....", // 4
            "K.Q.", // 3
            ".P..", // 2
            "....", // 1
        ];
        Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));
        List<Position> positions = puzzle.Moves(2, 'b');
        positions.Count.ShouldBe(2);
        positions.ShouldContain(new Position(3, 'a'));
        positions.ShouldContain(new Position(3, 'c'));
    }

    [Fact]
    public void pawn_cannot_skip_spaces()
    {
        string[] boardData = [
        //   abcd
            "NNNN", // 4
            ".N.N", // 3
            "NPNN", // 2
            "NNNN", // 1
        ];
        Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));
        List<Position> positions = puzzle.Moves(2, 'b');
        positions.Count.ShouldBe(0);
    }
}