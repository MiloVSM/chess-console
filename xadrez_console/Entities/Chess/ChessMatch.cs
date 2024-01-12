using GameBoard;

namespace Chess
{
    internal class ChessMatch
    {
        public Board ChessBoard { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool GameOver { get; private set; }

        public ChessMatch()
        {
            ChessBoard = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            GameOver = false;
            InitializePieces();
        }

        public void MovePiece(Position origin, Position destination)
        {
            Piece p = ChessBoard.RemovePiece(origin);
            p.IncrementMoves();
            Piece capturedPiece = ChessBoard.RemovePiece(destination);
            ChessBoard.PositionPiece(p, destination);
        }

        public void ExecutePlayerMove(Position origin, Position destination)
        {
            MovePiece(origin, destination);
            Turn++;
            switchPlayer();
        }

        // Verifica se a posição de origem é válida
        public void OriginValidation(Position position) {
            if (ChessBoard.GetPiece(position) == null)
            {
                throw new BoardException("Não existe peça na posição selecionada! Tente Novamente");
            }
            if (CurrentPlayer != ChessBoard.GetPiece(position).Color)
            {
                throw new BoardException("A peça na origem escolhida não é sua! Você só pode mover " + TranslateColor() + "!");
            }
            if (!ChessBoard.GetPiece(position).PossibleMovesExists())
            {
                throw new PositionException("A peça selecionada não possuí movimentos possíveis! Tente novamente!");
            }
        }

        // Verifica se a posição de destino é válida
        public void DestinationValidation(Position origin, Position destination)
        {
            if (!ChessBoard.PositionIsValid(destination))
            {
                throw new PositionException("Posição de Destino Inválida! Tente Novamente!");
            }
            if (!ChessBoard.GetPiece(origin).CanMoveTo(destination))
            {
                throw new PositionException("Posição de Destino Inválida! Tente Novamente!");
            }
        }

        private void switchPlayer()
        {
            if (CurrentPlayer == Color.White)
            {
                CurrentPlayer = Color.Black;
            }
            else
            {
                CurrentPlayer = Color.White;
            }
        }

        // Traduz a cor das peças para pt-br
        public string TranslateColor()
        {
            if (CurrentPlayer == Color.White)
            {
                return "Peças Brancas";
            }
            else
            {
                return "Peças Pretas";
            }
        }


        public void InitializePieces()
        {
            ChessBoard.PositionPiece(new Pawn(ChessBoard, Color.Black), new ChessCoordinates('d', 6).ToPosition());
            ChessBoard.PositionPiece(new Pawn(ChessBoard, Color.White), new ChessCoordinates('c', 4).ToPosition());
            ChessBoard.PositionPiece(new Queen(ChessBoard, Color.White), new ChessCoordinates('d', 4).ToPosition());

        }

    }
}
