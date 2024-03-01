using GameBoard;

namespace Chess
{
    internal class King : Piece
    {
        private ChessMatch Match;

        public King(Board board, Color color, ChessMatch match) : base(board, color)
        {
            Match = match;
        }

        // Testa se a torre pode participar do roque
        private bool RookCanCastle(Position pos)
        {
            Piece p = Board.GetPiece(pos);
            return p != null && p is Rook && p.Color == this.Color && p.Movements == 0;
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
            pos.DefineValues(Position.Row - 1, Position.Column + 1);
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
            pos.DefineValues(Position.Row + 1, Position.Column + 1);
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

            
            if (this.Movements == 0 && !Match.InCheck)
            {
                // Jogada Especial: Roque pequeno
                // Kingslide castling
                Position rookPos = new Position(Position.Row, Position.Column + 3);
                if (RookCanCastle(rookPos))
                {
                    Position p1 = new Position(Position.Row, Position.Column + 1);
                    Position p2 = new Position(Position.Row, Position.Column + 2); 
                    if (Board.GetPiece(p1) == null && Board.GetPiece(p2) == null)
                    {
                        matrix[Position.Row, Position.Column + 2] = true;
                    }
                }

                // Jogada Especial: Roque grande
                // Queenslide castling
                Position rookPos2 = new Position(Position.Row, Position.Column - 4);
                if (RookCanCastle(rookPos))
                {
                    Position p1 = new Position(Position.Row, Position.Column - 1);
                    Position p2 = new Position(Position.Row, Position.Column - 2); 
                    Position p3 = new Position(Position.Row, Position.Column - 3); 
                    if (Board.GetPiece(p1) == null && Board.GetPiece(p2) == null && Board.GetPiece(p3) == null)
                    {
                        matrix[Position.Row, Position.Column - 2] = true;
                    }
                }
            }

            return matrix;
        }

        public override string ToString()
        {
            return "K";
        }
    }
}
