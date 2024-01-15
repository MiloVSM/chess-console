using GameBoard;

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

            // Jogada Especial: Roque Pequeno
            // Kingslide castling
            if (p is King && destination.Column == origin.Column + 2)
            {
                Position rookOrigin = new Position(origin.Row, origin.Column + 3);
                Position rookDestination = new Position(origin.Row, origin.Column + 1);
                Piece rookCastle = ChessBoard.RemovePiece(rookOrigin);
                rookCastle.IncrementMoves();
                ChessBoard.PositionPiece(rookCastle, rookDestination);

            }

            // Jogada Especial: Roque grande
            // Queenslide castling
            if (p is King && destination.Column == origin.Column - 2)
            {
                Position rookOrigin = new Position(origin.Row, origin.Column - 4);
                Position rookDestination = new Position(origin.Row, origin.Column - 1);
                Piece rookCastle = ChessBoard.RemovePiece(rookOrigin);
                rookCastle.IncrementMoves();
                ChessBoard.PositionPiece(rookCastle, rookDestination);

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

        // Desfaz a última jogada e retorna as peças para suas posições originais
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

            // Jogada Especial: Roque pequeno
            if (p is King && destination.Column == origin.Column + 2)
            {
                Position rookOrigin = new Position(origin.Row, origin.Column + 3);
                Position rookDestination = new Position(origin.Row, origin.Column + 1);
                Piece rookCastle = ChessBoard.RemovePiece(rookDestination);
                rookCastle.DecrementMoves();
                ChessBoard.PositionPiece(rookCastle, rookOrigin);

            }

            // Jogada Especial: Roque Grande
            if (p is King && destination.Column == origin.Column - 2)
            {
                Position rookOrigin = new Position(origin.Row, origin.Column - 4);
                Position rookDestination = new Position(origin.Row, origin.Column - 1);
                Piece rookCastle = ChessBoard.RemovePiece(rookDestination);
                rookCastle.DecrementMoves();
                ChessBoard.PositionPiece(rookCastle, rookOrigin);

            }
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

        // Retorna a cor adversária
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

        // Retorna o rei de determinada cor
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

        // Verifica se o Rei de determinada cor está em check
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

        // Verifica se determinada cor está em checkmate e termina o jogo caso o retorno seja verdadeiro
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

        // Instancia uma nova peça e a insere no tabuleiro
        public void AddNewPiece(char column, int row, Piece piece)
        {
            ChessBoard.PositionPiece(piece, new ChessCoordinates(column, row).ToPosition());
            Pieces.Add(piece);
        }

        // Inicializa as peças no ínicio da partida
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

            AddNewPiece('a', 1, new Rook(ChessBoard, Color.White));
            AddNewPiece('b', 1, new Knight(ChessBoard, Color.White));
            AddNewPiece('c', 1, new Bishop(ChessBoard, Color.White));
            AddNewPiece('d', 1, new Queen(ChessBoard, Color.White));
            AddNewPiece('e', 1, new King(ChessBoard, Color.White, this));
            AddNewPiece('f', 1, new Bishop(ChessBoard, Color.White));
            AddNewPiece('g', 1, new Knight(ChessBoard, Color.White));
            AddNewPiece('h', 1, new Rook(ChessBoard, Color.White));


            AddNewPiece('a', 7, new Pawn(ChessBoard, Color.Black));
            AddNewPiece('b', 7, new Pawn(ChessBoard, Color.Black));
            AddNewPiece('c', 7, new Pawn(ChessBoard, Color.Black));
            AddNewPiece('d', 7, new Pawn(ChessBoard, Color.Black));
            AddNewPiece('e', 7, new Pawn(ChessBoard, Color.Black));
            AddNewPiece('f', 7, new Pawn(ChessBoard, Color.Black));
            AddNewPiece('g', 7, new Pawn(ChessBoard, Color.Black));
            AddNewPiece('h', 7, new Pawn(ChessBoard, Color.Black));

            AddNewPiece('a', 8, new Rook(ChessBoard, Color.Black));
            AddNewPiece('b', 8, new Knight(ChessBoard, Color.Black));
            AddNewPiece('c', 8, new Bishop(ChessBoard, Color.Black));
            AddNewPiece('d', 8, new Queen(ChessBoard, Color.Black));
            AddNewPiece('e', 8, new King(ChessBoard, Color.Black, this));
            AddNewPiece('f', 8, new Bishop(ChessBoard, Color.Black));
            AddNewPiece('g', 8, new Knight(ChessBoard, Color.Black));
            AddNewPiece('h', 8, new Rook(ChessBoard, Color.Black));

        }

    }
}
