using Shouldly;

namespace CaptainCoder.ChessPuzzler.Logic.Tests;

public class KingMoveTests
{

    [Fact]
    public void king_cannot_skip_spaces()
    {
        string[] boardData = [
        //   abcde
            "BBBBB", // 5
            "B...B", // 4
            "B.K.B", // 3
            "B...B", // 2
            "BBBBB", // 1
        ];
        Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));
        puzzle.Moves(3, 'c').ShouldBeEmpty();
    }

    [Fact]
    public void king_can_move_orthogonally()
    {
        string[] boardData = [
        //   abcd
            "....", // 4
            "..B.", // 3
            ".BKB", // 2
            "..B.", // 1
        ];
        Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));
        List<Position> moves = puzzle.Moves(2, 'c');
        moves.Count.ShouldBe(4);
        moves.ShouldContain(new Position(3, 'c'));
        moves.ShouldContain(new Position(2, 'b'));
        moves.ShouldContain(new Position(2, 'd'));
        moves.ShouldContain(new Position(1, 'c'));
    }

    [Fact]
    public void king_can_move_diagonally()
    {
        string[] boardData = [
        //   abcd
            "....", // 4
            ".B.B", // 3
            "..K.", // 2
            ".B.B", // 1
        ];
        Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));
        List<Position> moves = puzzle.Moves(2, 'c');
        moves.Count.ShouldBe(4);
        moves.ShouldContain(new Position(3, 'b'));
        moves.ShouldContain(new Position(3, 'd'));
        moves.ShouldContain(new Position(1, 'b'));
        moves.ShouldContain(new Position(1, 'd'));
    }

    [Fact]
    public void king_can_move_in_all_directions()
    {
        string[] boardData = [
        //   abcd
            "....", // 4
            ".BBB", // 3
            ".BKB", // 2
            ".BBB", // 1
        ];
        Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));
        List<Position> moves = puzzle.Moves(2, 'c');
        moves.Count.ShouldBe(8);
        moves.ShouldContain(new Position(3, 'b'));
        moves.ShouldContain(new Position(3, 'c'));
        moves.ShouldContain(new Position(3, 'd'));
        moves.ShouldContain(new Position(2, 'b'));
        moves.ShouldContain(new Position(2, 'd'));
        moves.ShouldContain(new Position(1, 'b'));
        moves.ShouldContain(new Position(1, 'c'));
        moves.ShouldContain(new Position(1, 'd'));
    }
}