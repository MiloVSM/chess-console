using GameBoard;

namespace Chess
{
    internal class King : Piece
    {
        public King(Board board, Color color) : base(board, color)
        {
        }

        // Lógica de movimentos possíveis do Rei
        public override bool[,] PossibleMoves()
        {
            bool[,] matrix = new bool[Board.Columns, Board.Rows];
            Position pos = new Position(0, 0);

            // Posição Norte (acima)
            pos.DefineValues(Position.Row - 1, Position.Column);
            if (Board.PositionIsValid(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }   
            // Posição Nordeste (⬔)
            pos.DefineValues(Position.Row - 1, Position.Column +1);
            if (Board.PositionIsValid(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }
            // Posição Leste (direita)
            pos.DefineValues(Position.Row, Position.Column + 1);
            if (Board.PositionIsValid(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }
            // Posição Sudeste (◪)
            pos.DefineValues(Position.Row +1, Position.Column + 1);
            if (Board.PositionIsValid(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }
            // Posição Sul (abaixo)
            pos.DefineValues(Position.Row + 1, Position.Column);
            if (Board.PositionIsValid(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }
            // Posição Sudoeste (⬕)
            pos.DefineValues(Position.Row + 1, Position.Column - 1);
            if (Board.PositionIsValid(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }
            // Posição Oeste (esquerda) 
            pos.DefineValues(Position.Row, Position.Column - 1);
            if (Board.PositionIsValid(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }
            // Posição Noroeste (◩) 
            pos.DefineValues(Position.Row - 1, Position.Column - 1);
            if (Board.PositionIsValid(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }
            return matrix;
        }

        public override string ToString()
        {
            return "K";
        }
    }
}
