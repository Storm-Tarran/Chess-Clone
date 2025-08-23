using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Windows.Input;
using ChessLogic;
using System.Drawing;
using System.Windows.Media;
using Point = System.Windows.Point;
using Rectangle = System.Windows.Shapes.Rectangle;
using Color = System.Windows.Media.Color;


namespace ChessUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //2D Array for the chessboard, 8x8
        private readonly Image[,] chessboardImages = new Image[8, 8];
        private readonly Rectangle[,] highlights = new Rectangle[8, 8];
        private readonly Dictionary<Position, Move> moveCache = new Dictionary<Position, Move>();

        private GameState gameState;
        private Position selectedPosition = null;

        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();

            gameState = new GameState(Player.White, Board.Initial());
            DrawBoard(gameState.Board);
            SetCursor(gameState.CurrentPlayer);
        }

        private void InitializeBoard()
        {
            //loop over all positions on the chessboard
            for(int row = 0; row < 8; row++ )
            {
                for(int col = 0; col < 8; col++)
                {
                    Image chessImage = new Image();
                    chessboardImages[row, col] = chessImage;
                    PieceGrid.Children.Add(chessImage);

                    Rectangle highlight = new Rectangle();
                    highlights[row,col] = highlight;
                    HighlightGrid.Children.Add(highlight);
                }
            }
        }

        //Method to draw board
        public void DrawBoard(Board board)
        {
            for(int row = 0; row < 8; row++)
            {
                for(int col = 0; col < 8; col++)
                {
                    //Grab the piece aat that position
                    Piece piece = board[row, col];
                    chessboardImages[row, col].Source = Images.GetImageSource(piece);
                }
            }
        }

        private void BoardGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(IsMenuOnScreen())
            {
                //Ignore clicks when menu is on screen
                return;
            }

            //Called when player clicks on the board
            Point point = e.GetPosition(BoardGrid);
            Position pos = ToSquarePosition(point);

            if(selectedPosition == null)
            {
                OnFromPositionSelected(pos);
            }
            else
            {
                OnToPositionSelected(pos);
            }
        }

        private Position ToSquarePosition(Point point)
        {
            double squareSize = BoardGrid.ActualWidth / 8;
            int row = (int)(point.Y / squareSize);
            int col = (int)(point.X / squareSize);
            return new Position(row, col);
        }

        private void OnFromPositionSelected(Position pos)
        {
            IEnumerable<Move> moves = gameState.LegalMovesForPieces(pos);

            if(moves.Any())
            {
                selectedPosition = pos;
                CacheMoves(moves);
                ShowHighlights();
            }
        }

        private void OnToPositionSelected(Position pos)
        {
            selectedPosition = null;
            ClearHighlights();

            if(moveCache.TryGetValue(pos, out Move move))
            {
                if(move.Type == MoveType.Promotion)
                {
                    HandelPromotion(move.FromPos, move.ToPos);
                }
                else
                { 
                    HandelMove(move);
                }

            }
            //moveCache.Clear();
        }

        private void HandelPromotion(Position from, Position to)
        {
            //CHECK
            chessboardImages[to.Row, to.Column].Source = Images.GetImageSource(gameState.CurrentPlayer, PieceType.Pawn);
            chessboardImages[from.Row, from.Column].Source = null;

            PromotionMenu promotionMenu = new PromotionMenu(gameState.CurrentPlayer);
            MenuContainer.Content = promotionMenu;

            promotionMenu.PieceSelected += type =>
            {
                MenuContainer.Content = null;
                Move promotionMove = new PawnPromotion(from, to, type);
                HandelMove(promotionMove);
            };
        }

        private void HandelMove(Move move)
        {
            //For future use, e.g., animations or sound effects
            gameState.MakeMove(move);
            DrawBoard(gameState.Board);
            SetCursor(gameState.CurrentPlayer);

            if(gameState.IsGameOver())
            {
                ShowGameOver();
            }
        }

        private void CacheMoves(IEnumerable<Move> moves)
        {
            moveCache.Clear();
            foreach(Move move in moves)
            {
                moveCache[move.ToPos] = move;
            }
        }

        private void ShowHighlights()
        {
            Color color = Color.FromArgb(140, 0, 255, 0);

            foreach(Position to in moveCache.Keys)
            {
                highlights[to.Row, to.Column].Fill = new SolidColorBrush(color);

            }
        }

        private void ClearHighlights()
        {
            foreach(Position to in moveCache.Keys)
            {
                highlights[to.Row, to.Column].Fill = Brushes.Transparent;
            }
        }

        private void SetCursor(Player player)
        {
            if(player == Player.White)
            {
                Cursor = ChessCursors.WhiteCursor;
            }
            else
            {
                Cursor = ChessCursors.BlackCursor;
            }
        }

        private bool IsMenuOnScreen()
        {
            return MenuContainer.Content != null;
        }

        private void ShowGameOver()
        {
            GameOverMenu gameOverMenu = new GameOverMenu(gameState);
            MenuContainer.Content = gameOverMenu;

            gameOverMenu.OptionSelected += option =>
            {
                if (option == Option.Restart)
                {
                    MenuContainer.Content = null;
                    RestartGame();
                }
                else
                {
                    Application.Current.Shutdown();
                }
            };
        }

        private void RestartGame()
        {
            ClearHighlights();
            moveCache.Clear();
            gameState = new GameState(Player.White, Board.Initial());
            DrawBoard(gameState.Board);
            SetCursor(gameState.CurrentPlayer);
        }
    }
}