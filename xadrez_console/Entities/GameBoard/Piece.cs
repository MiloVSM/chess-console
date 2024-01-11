namespace GameBoard
{
    internal class Piece
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
    }
}
