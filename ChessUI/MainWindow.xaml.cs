using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChessLogic;

namespace ChessUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //2D Array for the chessboard, 8x8
        private readonly Image[,] chessboardImages = new Image[8, 8];

        private GameState gameState;
        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();

            gameState = new GameState(Player.White, Board.Initial());
            DrawBoard(gameState.Board);
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
    }
}