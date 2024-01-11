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

            ChessCoordinates coord = new ChessCoordinates('c', 7);
            Console.WriteLine(coord);
            Console.WriteLine(coord.ToPosition());
            Console.ReadLine();
        }
    }
}   