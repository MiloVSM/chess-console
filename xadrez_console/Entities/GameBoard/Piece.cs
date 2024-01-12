namespace GameBoard
{
    abstract class Piece
    {
        public Position? Position { get; set; }
        public Color Color { get; protected set; }
        public int Movements { get; set; }
        public Board Board { get; protected set; }

        public Piece(Board board, Color color)
        {
            Board = board;
            Color = color;
            Movements = 0;
        }

        public void IncrementMoves()
        {
            Movements++;
        }

        public void DecrementMoves()
        {
            Movements--;
        }

        // Testa se as possíveis posições permitem o movimento (vazia ou contém peça inimiga)
        public virtual bool CanMove(Position position)
        {
            Piece piece = Board.GetPiece(position);
            return piece == null || piece.Color != this.Color;
        }

        public bool CanMoveTo(Position position)
        {
            return PossibleMoves()[position.Row, position.Column];
        }

        // Verifica se a peça selecionada possui movimentos possíveis (peça travada em todas as direções)
        public bool PossibleMovesExists()
        {
            bool[,] matrix = PossibleMoves();
            for (int i = 0; i < Board.Rows; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    if (matrix[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public abstract bool[,] PossibleMoves();
    }
}
