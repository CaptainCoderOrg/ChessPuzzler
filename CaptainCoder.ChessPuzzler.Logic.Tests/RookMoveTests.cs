using Shouldly;

namespace CaptainCoder.ChessPuzzler.Logic.Tests;

public class RookMoveTests
{

    [Fact]
    public void rook_cannot_move_diagonally()
    {
        string[] boardData = [
        //   abcde
            "PP.PP", // 5
            "PP.PP", // 4
            "..R..", // 3
            "PP.PP", // 2
            "PP.PP", // 1
        ];
        Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));
        List<Position> moves = puzzle.Moves(3, 'c');
        moves.Count.ShouldBe(0);
    }

    [Fact]
    public void rook_can_move_orthogonally_1()
    {
        string[] boardData = [
        //   abcde
            ".....", // 5
            "..P..", // 4
            ".PRP.", // 3
            "..P..", // 2
            ".....", // 1
        ];
        Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));
        List<Position> moves = puzzle.Moves(3, 'c');
        moves.Count.ShouldBe(4);
        moves.ShouldContain(new Position(3, 'b'));
        moves.ShouldContain(new Position(3, 'd'));
        moves.ShouldContain(new Position(4, 'c'));
        moves.ShouldContain(new Position(2, 'c'));
    }

    [Fact]
    public void rook_can_move_orthogonally_2()
    {
        string[] boardData = [
        //   abcde
            "..P..", // 5
            ".....", // 4
            "P.R.P", // 3
            ".....", // 2
            "..P..", // 1
        ];
        Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));
        List<Position> moves = puzzle.Moves(3, 'c');
        moves.Count.ShouldBe(4);
        moves.ShouldContain(new Position(3, 'a'));
        moves.ShouldContain(new Position(3, 'e'));
        moves.ShouldContain(new Position(5, 'c'));
        moves.ShouldContain(new Position(1, 'c'));
    }

    [Fact]
    public void rook_cannot_jump_over_pieces()
    {
        string[] boardData = [
        //   abcde
            "..P..", // 5
            "..P..", // 4
            "PPRPP", // 3
            "..P..", // 2
            "..P..", // 1
        ];
        Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));
        List<Position> moves = puzzle.Moves(3, 'c');
        moves.Count.ShouldBe(4);
        moves.ShouldContain(new Position(3, 'b'));
        moves.ShouldContain(new Position(3, 'd'));
        moves.ShouldContain(new Position(4, 'c'));
        moves.ShouldContain(new Position(2, 'c'));
    }

    [Fact]
    public void rook_can_move_orthogonally_any_distance()
    {
        string[] boardData = [
        //   abcdefgh
            "R......R", // 8
            "........", // 7
            "........", // 6
            "........", // 5
            "........", // 4
            "........", // 3
            "........", // 2
            "R......R", // 1
        ];
        Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));
        puzzle.Moves(8, 'a').Count.ShouldBe(2);
        puzzle.Moves(8, 'a').ShouldContain(new Position(8, 'h'));
        puzzle.Moves(8, 'a').ShouldContain(new Position(1, 'a'));

        puzzle.Moves(1, 'a').Count.ShouldBe(2);
        puzzle.Moves(1, 'a').ShouldContain(new Position(1, 'h'));
        puzzle.Moves(1, 'a').ShouldContain(new Position(8, 'a'));

        puzzle.Moves(8, 'h').Count.ShouldBe(2);
        puzzle.Moves(8, 'h').ShouldContain(new Position(8, 'a'));
        puzzle.Moves(8, 'h').ShouldContain(new Position(1, 'h'));

        puzzle.Moves(1, 'h').Count.ShouldBe(2);
        puzzle.Moves(1, 'h').ShouldContain(new Position(8, 'h'));
        puzzle.Moves(1, 'h').ShouldContain(new Position(1, 'a'));
    } 
}