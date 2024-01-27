using Shouldly;

namespace CaptainCoder.ChessPuzzler.Logic.Tests;

public class BishopMoveTests
{

    [Fact]
    public void bishop_cannot_move_orthogonally()
    {
        string[] boardData = [
        //   abcde
            ".PPP.", // 5
            "P.P.P", // 4
            "PPBPP", // 3
            "P.P.P", // 2
            ".PPP.", // 1
        ];
        Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));
        puzzle.Moves(3, 'c').ShouldBeEmpty();
    }

    [Fact]
    public void bishop_can_move_diagonally_2()
    {
        string[] boardData = [
        //   abcde
            "P...P", // 5
            ".....", // 4
            "..B..", // 3
            ".....", // 2
            "P...P", // 1
        ];
        Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));
        List<Position> moves = puzzle.Moves(3, 'c');
        moves.Count.ShouldBe(4);
        moves.ShouldContain(new Position(5, 'a'));
        moves.ShouldContain(new Position(5, 'e'));
        moves.ShouldContain(new Position(1, 'a'));
        moves.ShouldContain(new Position(1, 'e'));
    }

    [Fact]
    public void bishop_can_move_diagonally_1()
    {
        string[] boardData = [
        //   abcde
            ".....", // 5
            ".P.P.", // 4
            "..B..", // 3
            ".P.P.", // 2
            ".....", // 1
        ];
        Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));
        List<Position> moves = puzzle.Moves(3, 'c');
        moves.Count.ShouldBe(4);
        moves.ShouldContain(new Position(4, 'b'));
        moves.ShouldContain(new Position(4, 'd'));
        moves.ShouldContain(new Position(2, 'b'));
        moves.ShouldContain(new Position(2, 'd'));
    }

    [Fact]
    public void bishop_cannot_jump_over_pieces()
    {
        string[] boardData = [
        //   abcde
            "P...P", // 5
            ".P.P.", // 4
            "..B..", // 3
            ".P.P.", // 2
            "P...P", // 1
        ];
        Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));
        List<Position> moves = puzzle.Moves(3, 'c');
        moves.Count.ShouldBe(4);
        moves.ShouldContain(new Position(4, 'b'));
        moves.ShouldContain(new Position(4, 'd'));
        moves.ShouldContain(new Position(2, 'b'));
        moves.ShouldContain(new Position(2, 'd'));
    }

    [Fact]
    public void bishop_can_move_diagonally_any_distance()
    {
        string[] boardData = [
        //   abcdefgh
            "B......B", // 8
            "........", // 7
            "........", // 6
            "........", // 5
            "........", // 4
            "........", // 3
            "........", // 2
            "B......B", // 1
        ];
        Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));
        puzzle.Moves(8, 'a').Count.ShouldBe(1);
        puzzle.Moves(8, 'a').ShouldContain(new Position(1, 'h'));

        puzzle.Moves(8, 'h').Count.ShouldBe(1);
        puzzle.Moves(8, 'h').ShouldContain(new Position(1, 'a'));

        puzzle.Moves(1, 'a').Count.ShouldBe(1);
        puzzle.Moves(1, 'a').ShouldContain(new Position(8, 'h'));

        puzzle.Moves(1, 'h').Count.ShouldBe(1);
        puzzle.Moves(1, 'h').ShouldContain(new Position(8, 'a'));
    }

    
}