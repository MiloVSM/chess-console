using GameBoard;

namespace Chess
{
    internal class Rook : Piece
    {
        public Rook(Board board, Color color) : base(board, color)
        {
        }

        // Lógica de movimentos possíveis da Torre
        public override bool[,] PossibleMoves()
        {
            bool[,] matrix = new bool[Board.Rows, Board.Columns]; // Corrigindo a ordem das dimensões da matriz
            Position pos = new Position(0, 0);

            // Posição Norte (acima)
            pos.DefineValues(Position.Row - 1, Position.Column); // Corrigindo o acesso à propriedade Position
            while (Board.PositionIsValid(pos) && CanMove(pos)) // Corrigindo a condição do loop
            {
                matrix[pos.Row, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != this.Color) // Corrigindo a condição de parada
                {
                    break;
                }
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
        return "T";
    }
}
}
