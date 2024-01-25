/// <summary>
/// Represents a chess board with dynamic size.
/// </summary>
public class Board
{
    private ChessPiece?[,] _data;
    /// <summary>
    /// Gets the size of the board (both width and height).
    /// </summary>
    public int Size { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Board"/> class with a specified size.
    /// </summary>
    /// <param name="size">The size of the board. Must be between 2 and 26.</param>
    /// <exception cref="ArgumentException">Thrown when the size is not between 2 and 26.</exception>
    public Board(int size)
    {
        if (size < 2 || size > 26) { throw new ArgumentException("Board Size must be between 2 and 26."); }
        Size = size;
        _data = new ChessPiece?[Size, Size];
    }

    /// <summary>
    /// Gets a list of ranks on the board.
    /// </summary>
    /// <returns>A list of integers representing the ranks.</returns>
    public List<int> Ranks()
    {
        List<int> ranks = new();
        for (int rank = Size; rank >= 1; rank--)
        {
            ranks.Add(rank);
        }
        return ranks;
    }

    /// <summary>
    /// Gets a list of files on the board.
    /// </summary>
    /// <returns>A list of characters representing the files.</returns>
    public List<char> Files()
    {
        List<char> files = new();
        for (int file = 0; file < Size; file++)
        {
            files.Add((char)('a' + file));
        }
        return files;
    }

    /// <summary>
    /// Removes a chess piece from the specified position on the board.
    /// </summary>
    /// <param name="rank">The rank from which to remove the piece.</param>
    /// <param name="file">The file from which to remove the piece.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the specified position is out of bounds.</exception>
    /// <exception cref="InvalidOperationException">Thrown if there is no piece at the specified position.</exception>
    public void RemovePiece(int rank, char file)
    {
        if (!InBounds(rank, file)) { throw new ArgumentOutOfRangeException(); }
        if (!_data[rank - 1, file - 'a'].HasValue) { throw new InvalidOperationException(); }
        _data[rank - 1, file - 'a'] = null;
    }

    /// <summary>
    /// Sets a chess piece at the specified position on the board.
    /// </summary>
    /// <param name="rank">The rank at which to place the piece.</param>
    /// <param name="file">The file at which to place the piece.</param>
    /// <param name="piece">The chess piece to place on the board.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the specified position is out of bounds.</exception>
    public void SetPiece(int rank, char file, ChessPiece piece)
    {
        if (!InBounds(rank, file)) { throw new ArgumentOutOfRangeException(); }
        _data[rank - 1, file - 'a'] = piece;
    }

    /// <summary>
    /// Gets the chess piece at the specified position on the board.
    /// </summary>
    /// <param name="rank">The rank of the piece to get.</param>
    /// <param name="file">The file of the piece to get.</param>
    /// <returns>The chess piece at the specified position.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the specified position is out of bounds.</exception>
    /// <exception cref="InvalidOperationException">Thrown if there is no piece at the specified position.</exception>
    public ChessPiece GetPiece(int rank, char file)
    {
        if (!InBounds(rank, file)) { throw new ArgumentOutOfRangeException(); }
        if (!_data[rank - 1, file - 'a'].HasValue) { throw new InvalidOperationException(); }
        return _data[rank - 1, file - 'a']!.Value;
    }

    /// <summary>
    /// Checks if a specified position on the board is occupied by a chess piece.
    /// </summary>
    /// <param name="rank">The rank to check.</param>
    /// <param name="file">The file to check.</param>
    /// <returns>True if the position is occupied; otherwise, false.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the specified position is out of bounds.</exception>
    public bool IsOccupied(int rank, char file)
    {
        if (!InBounds(rank, file)) { throw new ArgumentOutOfRangeException(); }
        return _data[rank - 1, file - 'a'].HasValue;
    }

    /// <summary>
    /// Determines whether the specified rank and file are within the bounds of the board.
    /// </summary>
    /// <param name="rank">The rank to check.</param>
    /// <param name="file">The file to check.</param>
    /// <returns>True if the position is within bounds; otherwise, false.</returns>
    private bool InBounds(int rank, char file) => rank >= 1 && rank <= Size && (file - 'a') >= 0 && (file - 'a') < Size;
}