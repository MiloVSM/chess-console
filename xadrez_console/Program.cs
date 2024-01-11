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
                Board chessBoard = new Board(8, 8);

                chessBoard.PositionPiece(new Rook(chessBoard, Color.Black), new Position(0, 0));
                chessBoard.PositionPiece(new Rook(chessBoard, Color.Black), new Position(1, 3));
                chessBoard.PositionPiece(new King(chessBoard, Color.Black), new Position(0, 1));

                Screen.PrintBoard(chessBoard);
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
    }
}