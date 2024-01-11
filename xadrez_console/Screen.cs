using Chess;
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
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(8 - i + " ");

                for (int j = 0; j < board.Columns; j++)
                {
                    PrintPiece(board.GetPiece(i, j));
                }
                Console.WriteLine();
            }
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("  a b c d e f g h");
            Console.ForegroundColor = aux;
        }

        // Método de sobrecarga para visualizar as posições possíveis
        public static void PrintBoard(Board board, bool[,] possiblePositions)
        {
            ConsoleColor bgColor = Console.BackgroundColor;
            ConsoleColor previewColor = ConsoleColor.DarkGray;

            for (int i = 0; i < board.Rows; i++)
            {

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(8 - i + " ");

                for (int j = 0; j < board.Columns; j++)
                {
                    if (possiblePositions[i, j])
                    {
                        Console.BackgroundColor = previewColor;
                    }
                    else
                    {
                        Console.BackgroundColor = bgColor;
                    }
                    PrintPiece(board.GetPiece(i, j));
                    Console.BackgroundColor = bgColor;
                }
                Console.WriteLine();
            }
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("  a b c d e f g h");
            Console.ForegroundColor = aux;
            Console.BackgroundColor = bgColor;
        }

        // Lê o input do usuário e converte o string para coordenadas
        public static ChessCoordinates ReadCoordinates()
        {
            string input = Console.ReadLine();
            char column = input[0];
            int row = int.Parse(input[1].ToString());
            ChessCoordinates coords = new ChessCoordinates(column, row);
            return coords;
        }

        // Testa se existe uma peça na posição e imprimi a peça na cor especificada de acordo com o lado
        public static void PrintPiece(Piece piece)
        {
            // Lado Branco = player 1 // Lado Preto = player 2

            ConsoleColor player1_color = ConsoleColor.Green;
            ConsoleColor player2_color = ConsoleColor.DarkYellow;

            if (piece == null)
            {
                Console.Write("- ");
                return;
            }

            ConsoleColor color = (piece.Color == Color.White) ? player1_color : player2_color;

            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(piece + " ");
            Console.ForegroundColor = aux;
        }
    }
}
