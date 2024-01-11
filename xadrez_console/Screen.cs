using GameBoard;
using System.Reflection.PortableExecutable;

namespace xadrez_console
{
    class Screen
    {
        // Imprimi o tabuleiro no console
        public static void PrintBoard(Board board)
        {
            for (int i = 0; i < board.Rows; i++)
            {
                ConsoleColor aux1 = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(8 - i + " ");
                Console.ForegroundColor = aux1;

                for (int j = 0; j < board.Columns; j++)
                {
                    if (board.GetPiece(i, j) != null) 
                    {
                        PrintPiece(board.GetPiece(i, j));
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.Write("- ");
                    }
                    
                }
                Console.WriteLine();
            }
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("  a b c d e f g h");
            Console.ForegroundColor = aux;
        }

        public static void PrintPiece(Piece piece)
        {
            if (piece.Color == Color.White)
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(piece);
                Console.ForegroundColor = aux;
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(piece);
                Console.ForegroundColor = aux;
            }
        }
    }
}
