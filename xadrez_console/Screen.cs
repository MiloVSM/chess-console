using ChessBoard;

namespace xadrez_console
{
    class Screen
    {
        // Imprimi o tabuleiro no console
        public static void PrintBoard(Board board)
        {
            for (int i = 0; i < board.Rows; i++)
            {
                for(int j = 0; j < board.Columns; j++)
                {
                    if (board.GetPiece(i, j) != null) 
                    {
                        Console.Write(board.GetPiece(i, j) + " ");
                    }
                    else
                    {
                        Console.Write("- ");
                    }
                    
                }
                Console.WriteLine();
            }
        }
    }
}
