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
            // Console.OutputEncoding = System.Text.Encoding.Unicode; //( Habilita o uso de caracteres Unicode )

            try
            {
                ChessMatch chessMatch = new ChessMatch();

                // Gameloop
                while (!chessMatch.GameOver)
                {
                    Console.Clear();
                    Screen.PrintBoard(chessMatch.ChessBoard);

                    Console.WriteLine();
                    Console.Write("Origem: ");
                    Position origin = Screen.ReadCoordinates().ToPosition();
                    Console.Write("Destino: ");
                    Position destination = Screen.ReadCoordinates().ToPosition();

                    chessMatch.ExecuteMove(origin, destination);
                }
  

                
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
    }
}   