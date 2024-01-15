using GameBoard;
using Chess;
using System;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            // Testando o método que imprime o tabuleiro no console
            // Console.OutputEncoding = System.Text.Encoding.Unicode; //( Habilita o uso de caracteres Unicode   )
            Console.BackgroundColor = ConsoleColor.Black;

            // Instancia o objeto da partida de xadrez
            ChessMatch chessMatch = new ChessMatch();

            // Gameloop
            try {
                while (!chessMatch.GameOver)
                {
                    try
                    {
                        Console.Clear();
                        Screen.PrintMatch(chessMatch);

                        Console.WriteLine();
                        Console.Write("Origem: ");
                        Position origin = Screen.ReadCoordinates().ToPosition();
                        chessMatch.OriginValidation(origin);

                        Console.WriteLine();
                        bool[,] possiblePositions = chessMatch.ChessBoard.GetPiece(origin).PossibleMoves();

                        Console.Clear();
                        Screen.PrintBoard(chessMatch.ChessBoard, possiblePositions, chessMatch);

                        Console.WriteLine();
                        Console.WriteLine("Turno: " + chessMatch.Turn);
                        Console.WriteLine("Aguardando jogada: " + chessMatch.TranslateColor());
                        Console.WriteLine();
                        Console.Write("Destino: ");
                        Position destination = Screen.ReadCoordinates().ToPosition();
                        chessMatch.DestinationValidation(origin, destination);

                        chessMatch.ExecutePlayerMove(origin, destination);
                    }
                    catch (BoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                    catch (PositionException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("ERRO INESPERADO: " + e.Message);
                        Console.ReadLine();
                    }

                    Console.Clear();
                    Screen.PrintMatch(chessMatch);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
    }
}