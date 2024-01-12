using GameBoard;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chess
{
    internal class ChessMatch
    {
        public Board ChessBoard { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool GameOver { get; private set; }
        private HashSet<Piece> Pieces = new HashSet<Piece>();
        private HashSet<Piece> CollectedPieces = new HashSet<Piece>();

        public ChessMatch()
        {
            ChessBoard = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            GameOver = false;
            InitializePieces();
        }

        // Move a peça selecionada e remove as peças capturadas
        public void MovePiece(Position origin, Position destination)
        {
            Piece p = ChessBoard.RemovePiece(origin);
            p.IncrementMoves();
            Piece capturedPiece = ChessBoard.RemovePiece(destination);
            ChessBoard.PositionPiece(p, destination);
            if (capturedPiece != null)
            {
                CollectedPieces.Add(capturedPiece);
            }
        }

        // Executa o movimento do jogador
        public void ExecutePlayerMove(Position origin, Position destination)
        {
            MovePiece(origin, destination);
            Turn++;
            switchPlayer();
        }

        // Verifica se a posição de origem é válida
        public void OriginValidation(Position position)
        {
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

        // Alterna o turno entre peças brancas e pretas
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

        // Retorna o conjunto de peças capturadas da cor informada
        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> tmp = new HashSet<Piece>();
            foreach (Piece p in CollectedPieces)
            {
                if (p.Color == color)
                {
                    tmp.Add(p);
                }
            }
            return tmp;
        }

        // Retorna o conjunto de peças de determinada cor que não foram capturadas
        public HashSet<Piece> InGamePieces(Color color)
        {
            HashSet<Piece> tmp = new HashSet<Piece>();
            foreach (Piece p in Pieces)
            {
                if (p.Color == color)
                {
                    tmp.Add(p);
                }
            }
            tmp.ExceptWith(CapturedPieces(color));
            return tmp;
        }

        public void AddNewPiece(char column, int row, Piece piece)
        {
            ChessBoard.PositionPiece(piece, new ChessCoordinates(column, row).ToPosition());
            Pieces.Add(piece);
        }
        public void InitializePieces()
        {
            AddNewPiece('a', 2, new Pawn(ChessBoard, Color.White));
            AddNewPiece('b', 2, new Pawn(ChessBoard, Color.White));
            AddNewPiece('c', 2, new Pawn(ChessBoard, Color.White));
            AddNewPiece('d', 2, new Pawn(ChessBoard, Color.White));
            AddNewPiece('e', 2, new Pawn(ChessBoard, Color.White));
            AddNewPiece('f', 2, new Pawn(ChessBoard, Color.White));
            AddNewPiece('g', 2, new Pawn(ChessBoard, Color.White));
            AddNewPiece('h', 2, new Pawn(ChessBoard, Color.White));


            AddNewPiece('a', 7, new Pawn(ChessBoard, Color.Black));
            AddNewPiece('b', 7, new Pawn(ChessBoard, Color.Black));
            AddNewPiece('c', 7, new Pawn(ChessBoard, Color.Black));
            AddNewPiece('d', 7, new Pawn(ChessBoard, Color.Black));
            AddNewPiece('e', 7, new Pawn(ChessBoard, Color.Black));
            AddNewPiece('f', 7, new Pawn(ChessBoard, Color.Black));
            AddNewPiece('g', 7, new Pawn(ChessBoard, Color.Black));
            AddNewPiece('h', 7, new Pawn(ChessBoard, Color.Black));

        }

    }
}
