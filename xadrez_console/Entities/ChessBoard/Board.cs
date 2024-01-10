namespace ChessBoard
{
    internal class Board
    {
        //Determina o tamanho do tabuleiro ( Xadrez utiliza o padrão 8x8, outros jogos podem variar )
        public int Rows { get; set; }
        public int Columns { get; set; }

        // Matriz de peças
        private ChessPiece[,] pieces;

        public Board(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            pieces = new ChessPiece[rows, columns];
        }

        public ChessPiece GetPiece(int row, int column)
        {
            return pieces[row, column];
        }
    }
}
