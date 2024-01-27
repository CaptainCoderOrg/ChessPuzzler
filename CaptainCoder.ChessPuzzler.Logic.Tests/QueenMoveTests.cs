using Shouldly;

namespace CaptainCoder.ChessPuzzler.Logic.Tests;

public class QueenMoveTests
{

    [Fact]
    public void queen_can_move_diagonally_2()
    {
        string[] boardData = [
        //   abcde
            "P...P", // 5
            ".....", // 4
            "..Q..", // 3
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
    public void queen_can_move_diagonally_1()
    {
        string[] boardData = [
        //   abcde
            ".....", // 5
            ".P.P.", // 4
            "..Q..", // 3
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
    public void queen_can_move_all_dirs_1()
    {
        string[] boardData = [
        //   abcde
            ".....", // 5
            ".PPP.", // 4
            ".PQP.", // 3
            ".PPP.", // 2
            ".....", // 1
        ];
        Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));
        List<Position> moves = puzzle.Moves(3, 'c');
        moves.Count.ShouldBe(8);
        moves.ShouldContain(new Position(3, 'b'));
        moves.ShouldContain(new Position(3, 'd'));
        moves.ShouldContain(new Position(4, 'c'));
        moves.ShouldContain(new Position(2, 'c'));

        moves.ShouldContain(new Position(4, 'b'));
        moves.ShouldContain(new Position(4, 'd'));
        moves.ShouldContain(new Position(2, 'b'));
        moves.ShouldContain(new Position(2, 'd'));
    }

    [Fact]
    public void queen_cannot_jump_over_pieces()
    {
        string[] boardData = [
        //   abcde
            "P.P.P", // 5
            ".PPP.", // 4
            "PPQPP", // 3
            ".PPP.", // 2
            "P.P.P", // 1
        ];
        Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));
        List<Position> moves = puzzle.Moves(3, 'c');
        moves.Count.ShouldBe(8);
        moves.ShouldContain(new Position(3, 'b'));
        moves.ShouldContain(new Position(3, 'd'));
        moves.ShouldContain(new Position(4, 'c'));
        moves.ShouldContain(new Position(2, 'c'));

        moves.ShouldContain(new Position(4, 'b'));
        moves.ShouldContain(new Position(4, 'd'));
        moves.ShouldContain(new Position(2, 'b'));
        moves.ShouldContain(new Position(2, 'd'));
    }

    [Fact]
    public void rook_can_move_orthogonally_1()
    {
        string[] boardData = [
        //   abcde
            ".....", // 5
            "..P..", // 4
            ".PQP.", // 3
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
            "P.Q.P", // 3
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
    public void rook_can_move_all_dirs_2()
    {
        string[] boardData = [
        //   abcde
            "P.P.P", // 5
            ".....", // 4
            "P.Q.P", // 3
            ".....", // 2
            "P.P.P", // 1
        ];
        Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));
        List<Position> moves = puzzle.Moves(3, 'c');
        moves.Count.ShouldBe(8);
        moves.ShouldContain(new Position(3, 'a'));
        moves.ShouldContain(new Position(3, 'e'));
        moves.ShouldContain(new Position(5, 'c'));
        moves.ShouldContain(new Position(1, 'c'));
        moves.ShouldContain(new Position(5, 'a'));
        moves.ShouldContain(new Position(5, 'e'));
        moves.ShouldContain(new Position(1, 'a'));
        moves.ShouldContain(new Position(1, 'e'));
    }

    [Fact]
    public void queen_cannot_make_some_moves()
    {
        string[] boardData = [
        //   abcdefgh
            ".PP.PP.P", // 8
            "P.P.P.PP", // 7
            "PP...PPP", // 6
            "...Q....", // 5
            "PP...PPP", // 4
            "P.P.P.PP", // 3
            ".PP.PP.P", // 2
            "PPP.PPP.", // 1
        ];
        Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));
        puzzle.Moves(5, 'd').Count.ShouldBe(0);
    }
}