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

        // Testa se as possíveis posições permitem o movimenta (vazia ou contém peça inimiga)
        public bool CanMove(Position position)
        {
            Piece piece = Board.GetPiece(position);
            return piece == null || piece.Color != this.Color;
        }
        public abstract bool[,] PossibleMoves();
    }
}
