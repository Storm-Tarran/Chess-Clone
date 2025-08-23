using ChessLogic;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChessUI
{
    /// <summary>
    /// Interaction logic for PromotionMenu.xaml
    /// </summary>
    public partial class PromotionMenu : UserControl
    {
        public event Action<PieceType> PieceSelected;

        public PromotionMenu(Player player)
        {
            InitializeComponent();

            // Set images based on player color
            RookImage.Source = Images.GetImageSource(player, PieceType.Rook);
            KnightImage.Source = Images.GetImageSource(player, PieceType.Knight);
            BishopImage.Source = Images.GetImageSource(player, PieceType.Bishop);
            QueenImage.Source = Images.GetImageSource(player, PieceType.Queen);
        }

        private void QueenImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PieceSelected?.Invoke(PieceType.Queen);
        }

        private void RookImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PieceSelected?.Invoke(PieceType.Rook);
        }

        private void BishopImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PieceSelected?.Invoke(PieceType.Bishop);
        }

        private void KnightImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PieceSelected?.Invoke(PieceType.Knight);
        }
    }
}
