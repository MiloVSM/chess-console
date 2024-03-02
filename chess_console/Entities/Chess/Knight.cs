using GameBoard;

namespace Chess
{
    internal class Knight : Piece
    {
        public Knight(Board board, Color color) : base(board, color)
        {
        }

        // Lógica de movimentos possíveis do Cavalo
        public override bool[,] PossibleMoves()
        {
            bool[,] matrix = new bool[Board.Columns, Board.Rows];
            Position pos = new Position(0, 0);

            // Posição Norte-esquerda
            pos.DefineValues(Position.Row - 2, Position.Column -1);
            if (Board.PositionIsValid(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }
            // Posição Norte-direita
            pos.DefineValues(Position.Row - 2, Position.Column + 1);
            if (Board.PositionIsValid(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }
            // Posição Leste-Cima
            pos.DefineValues(Position.Row -1, Position.Column + 2);
            if (Board.PositionIsValid(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }
            // Posição Leste-Baixo
            pos.DefineValues(Position.Row + 1, Position.Column + 2);
            if (Board.PositionIsValid(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }
            // Posição Sul-Direita
            pos.DefineValues(Position.Row + 2, Position.Column + 1);
            if (Board.PositionIsValid(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }
            // Posição Sul-esquerda
            pos.DefineValues(Position.Row + 2, Position.Column - 1);
            if (Board.PositionIsValid(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }
            // Posição Oeste-Baixo
            pos.DefineValues(Position.Row + 1, Position.Column - 2);
            if (Board.PositionIsValid(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }
            // Posição Oeste-Cima
            pos.DefineValues(Position.Row - 1, Position.Column - 2);
            if (Board.PositionIsValid(pos) && CanMove(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }
            return matrix;
        }
        public override string ToString()
        {
            return "C";
        }
    }
}
