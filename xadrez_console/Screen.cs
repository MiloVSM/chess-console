using Chess;
using GameBoard;

namespace xadrez_console
{
    class Screen
    {
        private static ConsoleColor bgColor = Console.BackgroundColor;
        private static ConsoleColor fgColor = Console.ForegroundColor;

        public static void PrintMatch(ChessMatch chessMatch)
        {
            PrintBoard(chessMatch.ChessBoard, chessMatch);
            Console.WriteLine();
            PrintCapturedPieces(chessMatch);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Turno: " + chessMatch.Turn);
            Console.WriteLine("Aguardando jogada: " + chessMatch.TranslateColor());
            if (chessMatch.InCheck)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkRed;               
                Console.WriteLine("XEQUE! O REI DAS " + chessMatch.TranslateColor().ToUpper() + " ESTÁ EM XEQUE");
                Console.ForegroundColor = fgColor;
            }
        }

        public static void PrintCapturedPieces(ChessMatch chessMatch)
        {
            Console.WriteLine("Peças Capturadas: ");
            Console.Write("Brancas: ");
            Console.ForegroundColor = ConsoleColor.Green;
            PrintCollection(chessMatch.CapturedPieces(Color.White));
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Pretas: ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            PrintCollection(chessMatch.CapturedPieces(Color.Black));
            Console.ForegroundColor = ConsoleColor.White;

        }

        public static void PrintCollection(HashSet<Piece> collection)
        {
            Console.Write("[ ");
            foreach (Piece p in collection)
            {
                Console.Write(p + " ");
            }
            Console.Write(" ]");
        }


        // Imprimi o tabuleiro no console
        public static void PrintBoard(Board board, ChessMatch chessMatch)
        {
            for (int i = 0; i < board.Rows; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(8 - i + " ");

                for (int j = 0; j < board.Columns; j++)
                {
                    PrintPiece(board.GetPiece(i, j), chessMatch);
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("  a b c d e f g h");
            Console.ForegroundColor = fgColor;
        }

        // Método de sobrecarga para visualizar as posições possíveis
        public static void PrintBoard(Board board, bool[,] possiblePositions, ChessMatch chessMatch)
        {
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
                    PrintPiece(board.GetPiece(i, j), chessMatch);
                    Console.BackgroundColor = bgColor;
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("  a b c d e f g h");
            Console.ForegroundColor = fgColor;
            Console.BackgroundColor = bgColor;
        }

        // Lê o input do usuário e converte o string para coordenadas
        public static ChessCoordinates ReadCoordinates()
        {
            string input = Console.ReadLine().ToLower();
            char column = input[0];
            int row = int.Parse(input[1].ToString());
            ChessCoordinates coords = new ChessCoordinates(column, row);
            return coords;
        }

        // Testa se existe uma peça na posição e imprimi a peça na cor especificada de acordo com o lado
        public static void PrintPiece(Piece piece, ChessMatch chessMatch)
        {
            // Lado Branco = player 1 // Lado Preto = player 2

            ConsoleColor player1_color = ConsoleColor.Green;
            ConsoleColor player2_color = ConsoleColor.DarkYellow;

            ConsoleColor player1_CheckedColor = ConsoleColor.Green;
            ConsoleColor player2_CheckedColor = ConsoleColor.Yellow;

            if (piece == null)
            {
                Console.Write("- ");
                return;
            }

            ConsoleColor color = (piece.Color == Color.White) ? player1_color : player2_color;
            if (chessMatch.InCheck)
            {
                color = (piece.Color == Color.White) ? player1_CheckedColor : player2_CheckedColor;

                Piece checkedKing = chessMatch.CheckedKing;
                if (piece.Position == checkedKing.Position)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = color;
                    Console.Write(piece + " ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = bgColor;
                }
                else
                {
                    Console.ForegroundColor = color;
                    Console.Write(piece + " ");
                    Console.ForegroundColor = fgColor;
                }
            }
            else
            {
                Console.ForegroundColor = color;
                Console.Write(piece + " ");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
