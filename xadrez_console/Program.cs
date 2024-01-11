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
                chessBoard.PositionPiece(new Knight(chessBoard, Color.Black), new Position(0, 1));
                chessBoard.PositionPiece(new Bishop(chessBoard, Color.Black), new Position(0, 2));
                chessBoard.PositionPiece(new Queen(chessBoard, Color.Black), new Position(0, 3));
                chessBoard.PositionPiece(new King(chessBoard, Color.Black), new Position(0, 4));
                chessBoard.PositionPiece(new Bishop(chessBoard, Color.Black), new Position(0, 5));
                chessBoard.PositionPiece(new Knight(chessBoard, Color.Black), new Position(0, 6));
                chessBoard.PositionPiece(new Rook(chessBoard, Color.Black), new Position(0, 7));

                chessBoard.PositionPiece(new Pawn(chessBoard, Color.Black), new Position(1, 0));
                chessBoard.PositionPiece(new Pawn(chessBoard, Color.Black), new Position(1, 1));
                chessBoard.PositionPiece(new Pawn(chessBoard, Color.Black), new Position(1, 2));
                chessBoard.PositionPiece(new Pawn(chessBoard, Color.Black), new Position(1, 3));
                chessBoard.PositionPiece(new Pawn(chessBoard, Color.Black), new Position(1, 4));
                chessBoard.PositionPiece(new Pawn(chessBoard, Color.Black), new Position(1, 5));
                chessBoard.PositionPiece(new Pawn(chessBoard, Color.Black), new Position(1, 6));
                chessBoard.PositionPiece(new Pawn(chessBoard, Color.Black), new Position(1, 7));

                chessBoard.PositionPiece(new Pawn(chessBoard, Color.White), new Position(6, 0));
                chessBoard.PositionPiece(new Pawn(chessBoard, Color.White), new Position(6, 1));
                chessBoard.PositionPiece(new Pawn(chessBoard, Color.White), new Position(6, 2));
                chessBoard.PositionPiece(new Pawn(chessBoard, Color.White), new Position(6, 3));
                chessBoard.PositionPiece(new Pawn(chessBoard, Color.White), new Position(6, 4));
                chessBoard.PositionPiece(new Pawn(chessBoard, Color.White), new Position(6, 5));
                chessBoard.PositionPiece(new Pawn(chessBoard, Color.White), new Position(6, 6));
                chessBoard.PositionPiece(new Pawn(chessBoard, Color.White), new Position(6, 7));

                chessBoard.PositionPiece(new Rook(chessBoard, Color.White), new Position(7, 0));
                chessBoard.PositionPiece(new Knight(chessBoard, Color.White), new Position(7, 1));
                chessBoard.PositionPiece(new Bishop(chessBoard, Color.White), new Position(7, 2));
                chessBoard.PositionPiece(new Queen(chessBoard, Color.White), new Position(7, 3));
                chessBoard.PositionPiece(new King(chessBoard, Color.White), new Position(7, 4));
                chessBoard.PositionPiece(new Bishop(chessBoard, Color.White), new Position(7, 5));
                chessBoard.PositionPiece(new Knight(chessBoard, Color.White), new Position(7, 6));
                chessBoard.PositionPiece(new Rook(chessBoard, Color.White), new Position(7, 7));

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