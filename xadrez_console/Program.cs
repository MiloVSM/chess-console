using GameBoard;
using System;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            // Testando o método que imprime o tabuleiro no console
            Board chessBoard = new Board(8, 8);
            Screen.PrintBoard(chessBoard);

            Console.ReadLine();
        }
    }
}