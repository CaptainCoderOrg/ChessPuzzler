using Shouldly;

namespace CaptainCoder.ChessPuzzler.Logic.Tests;

public class KnightMoveTests
{

    [Fact]
    public void knight_has_no_moves_when_targets_empty()
    {
        string[] boardData = [
        //   abcd
            ".BB.", // 4
            "BBNB", // 3
            ".BBB", // 2
            "B.B.", // 1
        ];
        Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));
        puzzle.Moves(3, 'c').ShouldBeEmpty();
    }

    [Fact]
    public void knight_moves_down_2_right_1()
    {
        string[] boardData = [
        //   abcd
            "....", // 4
            ".N..", // 3
            "....", // 2
            "..K.", // 1
        ];
        Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));
        List<Position> positions = puzzle.Moves(3, 'b');
        positions.Count.ShouldBe(1);
        positions.ShouldContain(new Position(1, 'c'));
    }

    [Fact]
    public void knight_moves_down_1_right_2()
    {
        string[] boardData = [
        //   abcd
            "....", // 4
            ".N..", // 3
            "...K", // 2
            "....", // 1
        ];
        Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));
        List<Position> positions = puzzle.Moves(3, 'b');
        positions.Count.ShouldBe(1);
        positions.ShouldContain(new Position(2, 'd'));
    }

    [Fact]
    public void knight_moves_down_2_left_1()
    {
        string[] boardData = [
        //   abcd
            "....", // 4
            "..N.", // 3
            "....", // 2
            ".K..", // 1
        ];
        Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));
        List<Position> positions = puzzle.Moves(3, 'c');
        positions.Count.ShouldBe(1);
        positions.ShouldContain(new Position(1, 'b'));
    }
    [Fact]
    public void knight_moves_down_1_left_2()
    {
        string[] boardData = [
        //   abcd
            "....", // 4
            "..N.", // 3
            "K...", // 2
            "....", // 1
        ];
        Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));
        List<Position> positions = puzzle.Moves(3, 'c');
        positions.Count.ShouldBe(1);
        positions.ShouldContain(new Position(2, 'a'));
    }

    [Fact]
    public void knight_moves_up_1_left_2()
    {
        string[] boardData = [
        //   abcd
            "K...", // 4
            "..N.", // 3
            "....", // 2
            "....", // 1
        ];
        Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));
        List<Position> positions = puzzle.Moves(3, 'c');
        positions.Count.ShouldBe(1);
        positions.ShouldContain(new Position(4, 'a'));
    }

    [Fact]
    public void knight_moves_up_2_left_1()
    {
        string[] boardData = [
        //   abcd
            ".K..", // 4
            "....", // 3
            "..N.", // 2
            "....", // 1
        ];
        Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));
        List<Position> positions = puzzle.Moves(2, 'c');
        positions.Count.ShouldBe(1);
        positions.ShouldContain(new Position(4, 'b'));
    }

    [Fact]
    public void knight_moves_up_2_right_1()
    {
        string[] boardData = [
        //   abcd
            "...K", // 4
            "....", // 3
            "..N.", // 2
            "....", // 1
        ];
        Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));
        List<Position> positions = puzzle.Moves(2, 'c');
        positions.Count.ShouldBe(1);
        positions.ShouldContain(new Position(4, 'd'));
    }
    [Fact]
    public void knight_moves_up_1_right_2()
    {
        string[] boardData = [
        //   abcd
            "....", // 4
            "...K", // 3
            ".N..", // 2
            "....", // 1
        ];
        Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));
        List<Position> positions = puzzle.Moves(2, 'b');
        positions.Count.ShouldBe(1);
        positions.ShouldContain(new Position(3, 'd'));
    }

    [Fact]
    public void knight_all_moves()
    {
        string[] boardData = [
        //   abcde
            ".B.B.", // 5
            "B...B", // 4
            "..N..", // 3
            "B...B", // 2
            ".B.B.", // 1
        ];
        Puzzle puzzle = new Puzzle(BoardReader.Read(boardData));
        List<Position> positions = puzzle.Moves(3, 'c');
        positions.Count.ShouldBe(8);
        positions.ShouldContain(new Position(5, 'b'));
        positions.ShouldContain(new Position(5, 'd'));
        positions.ShouldContain(new Position(4, 'a'));
        positions.ShouldContain(new Position(4, 'e'));
        positions.ShouldContain(new Position(2, 'a'));
        positions.ShouldContain(new Position(2, 'e'));
        positions.ShouldContain(new Position(1, 'b'));
        positions.ShouldContain(new Position(1, 'd'));
    }
}
