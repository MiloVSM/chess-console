﻿using Chess;
using GameBoard;

namespace xadrez_console
{
    class Screen
    {
        public static ConsoleColor bgColor { get; private set; }  = Console.BackgroundColor;
        public static ConsoleColor fgColor { get; private set; }  = Console.ForegroundColor;
        public static string LastMove { get; private set; }

        // Imprime a partida no console
        public static void PrintMatch(ChessMatch chessMatch)
        {
            PrintBoard(chessMatch.ChessBoard, chessMatch);
            Console.WriteLine();
            PrintCapturedPieces(chessMatch);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Turno: " + chessMatch.Turn);
            PrintSpecialMove(chessMatch);

            if (!chessMatch.GameOver)
            {
                Console.WriteLine("Aguardando jogada: " + chessMatch.TranslateColor());
                if (chessMatch.InCheck)
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("XEQUE! O REI DAS " + chessMatch.TranslateColor().ToUpper() + " ESTÁ EM XEQUE");
                    Console.ForegroundColor = fgColor;
                }
            }
            else
            {
                Console.WriteLine("XEQUEMATE!");
                Console.WriteLine("Vencedor: " + chessMatch.TranslateColor());
            }
        }

        // Imprime as peças capturadas de cada cor
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

        // Imprime um determinado conjunto de peças
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

        public static void PrintSpecialMove(ChessMatch chessMatch)
        {
            if (chessMatch.DidSpecialMove != null)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("JOGADA ESPECIAL: " + chessMatch.DidSpecialMove.ToString());
                Console.ForegroundColor = fgColor;
                Console.WriteLine();
            }
        }
        public static string GetPromotionInput(ChessMatch chessMatch)
        {
            Console.Write("Deseja promover o peão ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(LastMove);
            Console.ForegroundColor = fgColor;
            Console.Write(" para qual peça (Q/R/B/C)? ");
            string input = Console.ReadLine().Trim().ToUpper();
            return input;
        }


        // Lê o input do usuário e converte o string para coordenadas
        public static ChessCoordinates ReadCoordinates()
        {
            string input = Console.ReadLine().ToLower();
            char column = input[0];
            int row = int.Parse(input[1].ToString());
            ChessCoordinates coords = new ChessCoordinates(column, row);
            LastMove = input;
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
