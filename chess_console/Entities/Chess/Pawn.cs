using GameBoard;

namespace Chess
{
    internal class Pawn : Piece
    {
        private ChessMatch Match;
        public Pawn(Board board, Color color, ChessMatch match) : base(board, color)
        {
            Match = match;
        }

        // Testa se existe uma peça do lado oposto na posição especificada
        public bool ReacheableEnemy(Position position)
        {
            Piece piece = Board.GetPiece(position);
            return piece != null && piece.Color != this.Color;
        }

        // Determina a lógica de movimento de peão e retorna seus movimentos possíveis
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

                pos.DefineValues(Position.Row - 1, Position.Column - 1);
                if (Board.PositionIsValid(pos) && CanMove(pos) && ReacheableEnemy(pos))
                {
                    matrix[pos.Row, pos.Column] = true;
                }
                pos.DefineValues(Position.Row - 1, Position.Column + 1);
                if (Board.PositionIsValid(pos) && CanMove(pos) && ReacheableEnemy(pos))
                {
                    matrix[pos.Row, pos.Column] = true;
                }

                // Jogada Especial En Passant
                if (Position.Row == 3)
                {
                    Position left = new Position(Position.Row, Position.Column - 1);
                    Position right = new Position(Position.Row, Position.Column + 1);
                    if (Board.PositionIsValid(left) && ReacheableEnemy(left) && Board.GetPiece(left) == Match.EnPassantRisk)
                    {
                        matrix[left.Row - 1, left.Column] = true;
                    }
                    if (Board.PositionIsValid(right) && ReacheableEnemy(right) && Board.GetPiece(right) == Match.EnPassantRisk)
                    {
                        matrix[right.Row - 1, right.Column] = true;
                    }
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

                pos.DefineValues(Position.Row + 1, Position.Column - 1);
                if (Board.PositionIsValid(pos) && CanMove(pos) && ReacheableEnemy(pos))
                {
                    matrix[pos.Row, pos.Column] = true;
                }
                pos.DefineValues(Position.Row + 1, Position.Column + 1);
                if (Board.PositionIsValid(pos) && CanMove(pos) && Movements == 0 && ReacheableEnemy(pos))
                {
                    matrix[pos.Row, pos.Column] = true;
                }

                // Jogada Especial En Passant
                if (Position.Row == 4)
                {
                    Position left = new Position(Position.Row, Position.Column - 1);
                    Position right = new Position(Position.Row, Position.Column + 1);
                    if (Board.PositionIsValid(left) && ReacheableEnemy(left) && Board.GetPiece(left) == Match.EnPassantRisk)
                    {
                        matrix[left.Row + 1, left.Column] = true;
                    }
                    if (Board.PositionIsValid(right) && ReacheableEnemy(right) && Board.GetPiece(right) == Match.EnPassantRisk)
                    {
                        matrix[right.Row + 1, right.Column] = true;
                    }
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

