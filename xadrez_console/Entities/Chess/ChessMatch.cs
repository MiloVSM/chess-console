using GameBoard;

namespace Chess
{
    internal class ChessMatch
    {
        public Board ChessBoard { get; private set; }
        private int Turn;
        private Color CurrentPlayer;
        public bool GameOver { get; private set; }

        public ChessMatch()
        {
            ChessBoard = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            GameOver = false;
            InitializePieces();
        }

        public void ExecuteMove(Position origin, Position destination)
        {
            Piece p = ChessBoard.RemovePiece(origin);
            p.IncrementMoves();
            Piece capturedPiece = ChessBoard.RemovePiece(destination);
            ChessBoard.PositionPiece(p, destination);
        }

        public void InitializePieces()
        {
            ChessBoard.PositionPiece(new Rook(ChessBoard, Color.White), new ChessCoordinates('c', 1).ToPosition());
            
        }


    }
}
