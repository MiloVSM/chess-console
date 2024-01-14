using GameBoard;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.ConstrainedExecution;

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
        public bool InCheck { get; private set; }
        public Piece? CheckedKing { get; private set; }

        public ChessMatch()
        {
            ChessBoard = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            GameOver = false;
            InCheck = false;
            CheckedKing = null;
            InitializePieces();
        }

        // Move a peça selecionada e remove as peças capturadas
        public Piece MovePiece(Position origin, Position destination)
        {
            Piece p = ChessBoard.RemovePiece(origin);
            p.IncrementMoves();
            Piece capturedPiece = ChessBoard.RemovePiece(destination);
            ChessBoard.PositionPiece(p, destination);
            if (capturedPiece != null)
            {
                CollectedPieces.Add(capturedPiece);
            }
            return capturedPiece;
        }

        // Executa o movimento do jogador
        public void ExecutePlayerMove(Position origin, Position destination)
        {
            Piece capturedPiece = MovePiece(origin, destination);

            if (IsInCheck(CurrentPlayer))
            {
                UndoPlayerMove(origin, destination, capturedPiece);
                throw new BoardException("Você não pode se colocar em xeque! Faça outro movimento!");
            }
            if (IsInCheck(OpposingPlayer(CurrentPlayer)))
            {
                InCheck = true;
                CheckedKing = GetKing(OpposingPlayer(CurrentPlayer));
            }
            else
            {
                InCheck = false;
                CheckedKing = null;
            }

            if (Checkmate(OpposingPlayer(CurrentPlayer)))
            {
                GameOver = true;
            }
            else
            {
                Turn++;
                switchPlayer();
            }
        }

        public void UndoPlayerMove(Position origin, Position destination, Piece capturedPiece)
        {
            Piece p = ChessBoard.RemovePiece(destination);
            p.DecrementMoves();
            if (capturedPiece != null)
            {
                ChessBoard.PositionPiece(capturedPiece, destination);
                CollectedPieces.Remove(capturedPiece);
            }
            ChessBoard.PositionPiece(p, origin);
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
            if (!ChessBoard.GetPiece(origin).MoveIsPossible(destination))
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

        private Color OpposingPlayer(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

        public Piece GetKing(Color color)
        {
            foreach (Piece p in InGamePieces(color))
            {
                if (p is King)
                {
                    return p;
                }
            }
            return null;
        }

        private bool IsInCheck(Color color)
        {
            Piece K = GetKing(color);
            if (K == null)
            {
                throw new BoardException("Não tem rei da cor " + TranslateColor() + " no tabuleiro!");
            }
            foreach (Piece p in InGamePieces(OpposingPlayer(color)))
            {
                bool[,] matrix = p.PossibleMoves();
                if (matrix[K.Position.Row, K.Position.Column])
                {
                    return true;
                }
            }
            return false;
        }

        public bool Checkmate(Color color)
        {
            if (!IsInCheck(color))
            {
                return false;
            }
            foreach (Piece p in InGamePieces(color))
            {
                bool[,] matrix = p.PossibleMoves();
                for (int i = 0; i < ChessBoard.Rows; i++)
                {
                    for (int j = 0; j < ChessBoard.Columns; j++)
                    {
                        if (matrix[i, j])
                        {
                            Position origin = p.Position;
                            Position destination = new Position(i, j);
                            Piece capturedPiece = MovePiece(origin, destination);
                            bool stillInCheck = IsInCheck(color);
                            UndoPlayerMove(origin, destination, capturedPiece);
                            if (!stillInCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
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
            AddNewPiece('c', 1, new Rook(ChessBoard, Color.White));
            AddNewPiece('d', 1, new King(ChessBoard, Color.White));
            AddNewPiece('h', 7, new Rook(ChessBoard, Color.White));


            AddNewPiece('a', 8, new King(ChessBoard, Color.Black));
            AddNewPiece('b', 8, new Rook(ChessBoard, Color.Black));
        }

    }
}
