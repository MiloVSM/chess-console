using GameBoard;

namespace Chess
{
    internal class Pawn : Piece
    {
        public Pawn(Board board, Color color) : base(board, color)
        {
        }

        // Testa se existe uma peça do lado oposto na posição especificada
        public bool ReacheableEnemy(Position position)
        {
            Piece piece = Board.GetPiece(position);
            return piece != null && piece.Color != this.Color;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] matrix = new bool[Board.Columns, Board.Rows];
            Position pos = new Position(0, 0);

            if (Color == Color.White)
            {
                pos.DefineValues(Position.Row - 1, Position.Column);
                if (Board.PositionIsValid(pos) && CanMove(pos) && !ReacheableEnemy(pos))
                {
                    matrix[pos.Row, pos.Column] = true;
                }
                pos.DefineValues(Position.Row - 2, Position.Column);
                if (Board.PositionIsValid(pos) && CanMove(pos) && Movements == 0 && !ReacheableEnemy(pos))
                {
                    matrix[pos.Row, pos.Column] = true;
                }

                pos.DefineValues(Position.Row - 1, Position.Column -1);
                if (Board.PositionIsValid(pos) && CanMove(pos) && ReacheableEnemy(pos))
                {
                    matrix[pos.Row, pos.Column] = true;
                }
                pos.DefineValues(Position.Row - 1, Position.Column +1);
                if (Board.PositionIsValid(pos) && CanMove(pos) && ReacheableEnemy(pos))
                {
                    matrix[pos.Row, pos.Column] = true;
                }
            }
            else
            {
                pos.DefineValues(Position.Row + 1, Position.Column);
                if (Board.PositionIsValid(pos) && CanMove(pos) && !ReacheableEnemy(pos))
                {
                    matrix[pos.Row, pos.Column] = true;
                }
                pos.DefineValues(Position.Row + 2, Position.Column);
                if (Board.PositionIsValid(pos) && CanMove(pos) && Movements == 0 && !ReacheableEnemy(pos))
                {
                    matrix[pos.Row, pos.Column] = true;
                }

                pos.DefineValues(Position.Row + 1, Position.Column -1);
                if (Board.PositionIsValid(pos) && CanMove(pos) && ReacheableEnemy(pos))
                {
                    matrix[pos.Row, pos.Column] = true;
                }
                pos.DefineValues(Position.Row + 1, Position.Column + 1);
                if (Board.PositionIsValid(pos) && CanMove(pos) && Movements == 0 && ReacheableEnemy(pos))
                {
                    matrix[pos.Row, pos.Column] = true;
                }
            }
            return matrix;

        }
        public override string ToString()
        {
            return "P";
        }
    }
}

