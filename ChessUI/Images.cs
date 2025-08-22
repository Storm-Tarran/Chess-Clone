using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ChessLogic;

namespace ChessUI
{
    static class Images
    {
        //Load all images
        private static readonly Dictionary<PieceType, ImageSource> whitePieces = new()
        {
            { PieceType.Pawn, ImageSource("Assest/PawnW.png") },
            { PieceType.Rook, ImageSource("Assest/RookW.png") },
            { PieceType.Knight, ImageSource("Assest/KnightW.png") },
            { PieceType.Bishop, ImageSource("Assest/BishopW.png") },
            { PieceType.Queen, ImageSource("Assest/QueenW.png") },
            { PieceType.King, ImageSource("Assest/KingW.png") }
        };

        private static readonly Dictionary<PieceType, ImageSource> blackPieces = new()
        {
            { PieceType.Pawn, ImageSource("Assest/PawnB.png") },
            { PieceType.Rook, ImageSource("Assest/RookB.png") },
            { PieceType.Knight, ImageSource("Assest/KnightB.png") },
            { PieceType.Bishop, ImageSource("Assest/BishopB.png") },
            { PieceType.Queen, ImageSource("Assest/QueenB.png") },
            { PieceType.King, ImageSource("Assest/KingB.png") }
        };

        //Loain
        private static ImageSource ImageSource(string imageFileName)
        {
            return new BitmapImage(new Uri(imageFileName, UriKind.Relative));
        }

        //Get the image based on color and type
        public static ImageSource GetImageSource(Player color, PieceType type)
        {
            return color switch
            {
                Player.White => whitePieces[type],
                Player.Black => blackPieces[type],
                _ => null
            };
        }

        public static ImageSource GetImageSource(Piece piece)
        {
            if (piece == null)
            {
                return null;
            }
            return GetImageSource(piece.Color, piece.Type);
        }
    }
}
