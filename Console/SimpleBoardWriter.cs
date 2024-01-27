public class SimpleBoardWriter
{
    public void Write(Board board)
    {
        bool isBlack = true;
        Console.WriteLine($"   {string.Join(" ", board.Files())}");
        foreach (int rank in board.Ranks())
        {
            Console.Write($"{rank} ");
            if (board.Size % 2 == 0) { isBlack = !isBlack; }
            foreach (char file in board.Files())
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = isBlack ? ConsoleColor.DarkGray : ConsoleColor.White;
                if (board.IsOccupied(rank, file))
                {
                    ChessPiece piece = board.GetPiece(rank, file);
                    Console.Write($"{(char)piece} ");
                }
                else
                {
                    Console.Write("  ");
                }
                isBlack = !isBlack;
            }
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}