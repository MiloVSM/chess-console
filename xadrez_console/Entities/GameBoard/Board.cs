namespace GameBoard
{
    internal class Board
    {
        //Determina o tamanho do tabuleiro ( Xadrez utiliza o padrão 8x8, outros jogos podem variar )
        public int Rows { get; set; }
        public int Columns { get; set; }

        // Matriz de peças
        private Piece[,] pieces;

        public Board(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            pieces = new Piece[rows, columns];
        }

        public Piece GetPiece(int row, int column)
        {
            return pieces[row, column];
        }

        // Método para posicionar peças
        public void PositionPiece(Piece piece, Position position)
        {
            pieces[position.Row, position.Column] = piece;
            piece.Position = position;
        }
    }
}
