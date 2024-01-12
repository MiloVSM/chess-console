namespace GameBoard
{
    internal class Board
    {
        //Determina o tamanho do tabuleiro ( Xadrez utiliza o padrão 8x8, outros jogos podem variar )
        public int Rows { get; set; }
        public int Columns { get; set; }

        // Matriz de peças
        private Piece[,] pieces;

        //Construtor da Classe
        public Board(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            pieces = new Piece[rows, columns];
        }

        // Método para posição
        public Piece GetPiece(int row, int column)
        {
            return pieces[row, column];
        }

        // Sobrecarga utilizando a classe Position como argumento
        public Piece GetPiece(Position position)
        {
            ValidatePosition(position);
            return pieces[position.Row, position.Column];
        }

        public bool PositionOccupied(Position position)
        {
            ValidatePosition(position);
            return GetPiece(position) != null;
        }

        // Método para posicionar peças
        public void PositionPiece(Piece piece, Position position)
        {
                ValidatePosition(position);
                if (PositionOccupied(position))
                {
                    throw new BoardException("Já existe uma peça nessa posição!");
                }
                pieces[position.Row, position.Column] = piece;
                piece.Position = position;
        }

        // Método para retirar a peça do tabuleiro
        public Piece RemovePiece(Position position)
        {
            if (!PositionOccupied(position))
            {
                return null;
            }
            Piece aux = GetPiece(position);
            aux.Position = null;
            pieces[position.Row, position.Column] = null;
            return aux;
        }

        public bool PositionIsValid(Position position)
        {
            if (position.Row < 0 || position.Row >= Rows || position.Column < 0 || position.Column >= Columns)
            {
                return false;
            }
            return true;
        }

        public void ValidatePosition(Position position)
        {
            if (!PositionIsValid(position))
            {
                throw new BoardException("Posição Inválida! Tente Novamente!");
            }
        }
    }
}
