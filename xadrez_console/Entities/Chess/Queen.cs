using GameBoard;

namespace Chess
{
    internal class Queen : Piece
    {
        public Queen(Board board, Color color) : base(board, color)
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

            // Posição Norte (acima)
            pos.DefineValues(Position.Row - 1, Position.Column);
            while (Board.PositionIsValid(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != this.Color)
                {
                    break;
                }
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

            // Posição Leste (Direita)
            pos.DefineValues(Position.Row, Position.Column + 1);
            while (Board.PositionIsValid(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != this.Color)
                {
                    break;
                }
                pos.Column++;
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

            // Posição Sul (Abaixo)
            pos.DefineValues(Position.Row + 1, Position.Column);
            while (Board.PositionIsValid(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != this.Color)
                {
                    break;
                }
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

            // Posição Oeste (Esquerda)
            pos.DefineValues(Position.Row, Position.Column - 1);
            while (Board.PositionIsValid(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != this.Color)
                {
                    break;
                }
                pos.Column--;
            }

            return matrix;
        }

        public override string ToString()
        {
            return "D";
        }
    }
}

