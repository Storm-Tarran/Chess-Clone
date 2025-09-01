using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class PawnPromotion : Move
    {
        //Check end name
        public override MoveType Type => MoveType.Promotion;
        public override Position FromPos { get; }
        public override Position ToPos { get; }

        public readonly PieceType newType;

        public PawnPromotion(Position from, Position to, PieceType newType)
        {
            FromPos = from;
            ToPos = to;
            this.newType = newType;
        }

        private Piece CreatePromotionPiece(Player color)
        {
            return newType switch
            {
                PieceType.Rook => new Rook(color),
                PieceType.Bishop => new Bishop(color),
                PieceType.Knight => new Knight(color),
                _ => new Queen(color),
            };
        }

        public override bool Execute(Board board)
        {
            Piece pawn = board[FromPos];
            board[FromPos] = null; // Remove the pawn from its original position

            Piece promotedPiece = CreatePromotionPiece(pawn.Color);
            promotedPiece.HasMoved = true; // The promoted piece is considered to have moved
            board[ToPos] = promotedPiece; // Place the new piece at the promotion square

            return true;
        }
    }
}
