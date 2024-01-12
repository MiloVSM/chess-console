using System;
using GameBoard;

namespace Chess
{
    internal class Bishop : Piece
    {
        public Bishop(Board board, Color color) : base(board, color)
        {
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] matrix = new bool[Board.Columns, Board.Rows];
            Position pos = new Position(0, 0);

            // Movimento Noroeste (◩) 
            pos.DefineValues(Position.Row - 1, Position.Column - 1);
            while (Board.PositionIsValid(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != this.Color) // Corrigindo a condição de parada
                {
                    break;
                }
                pos.Column--;
                pos.Row--;
            }

            // Movimento Nordeste (⬔)
            pos.DefineValues(Position.Row - 1, Position.Column + 1);
            while (Board.PositionIsValid(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != this.Color) // Corrigindo a condição de parada
                {
                    break;
                }
                pos.Column++;
                pos.Row--;
            }

            // Posição Sudeste (◪)
            pos.DefineValues(Position.Row + 1, Position.Column + 1);
            while (Board.PositionIsValid(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != this.Color) // Corrigindo a condição de parada
                {
                    break;
                }
                pos.Column++;
                pos.Row++;
            }

            // Movimento Sudoeste (⬕)
            pos.DefineValues(Position.Row + 1, Position.Column - 1);
            while (Board.PositionIsValid(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != this.Color) // Corrigindo a condição de parada
                {
                    break;
                }
                pos.Column--;
                pos.Row++;
            }


            return matrix;
        }

        public override string ToString()
        {
            return "B";
        }
    }
}