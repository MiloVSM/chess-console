using GameBoard;

namespace Chess
{
    internal class ChessCoordinates
    {
        public char Column { get; set; }
        public int Row { get; set; }

        public ChessCoordinates(char column, int row)
        {
            Column = column;
            Row = row;
        }

        // Converte as coordenadas to xadrez para a posição na matriz
        public Position ToPosition()
        {
            return new Position(8 - Row, Column - 'a');
        }

        public override string ToString()
        {
            return Column + Row.ToString();
        }
    }
}
