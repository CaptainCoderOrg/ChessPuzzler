namespace ChessPuzzler;

public class FileSystemPuzzleLibrary : IPuzzleLibrary
{   
    public string BasePath { get; private set; }

    public FileSystemPuzzleLibrary(string basePath) => BasePath = basePath;
    public Puzzle LoadPuzzle(int id) => LoadPuzzle($"{BasePath}/{id.ToString().PadLeft(2, '0')}.txt");

    private static Puzzle LoadPuzzle(string filename)
    {
        string[] boardData = File.ReadAllLines(filename);
        return new Puzzle(BoardReader.Read(boardData));
    }

}