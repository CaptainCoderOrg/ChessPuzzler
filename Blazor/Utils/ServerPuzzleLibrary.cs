using ChessPuzzler;
public class ServerPuzzleLibrary : IPuzzleLibrary
{
    private string[] _puzzleData;

    public ServerPuzzleLibrary(string[] puzzleData)
    {
        _puzzleData = puzzleData;
    }

    public Puzzle LoadPuzzle(int id)
    {
        string data = _puzzleData[id-1];
        string[] ranks = data.ReplaceLineEndings().Split(Environment.NewLine);
        return new Puzzle(BoardReader.Read(ranks));
    }
}
