using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Pawn : Piece
    {
        public override PieceType Type => PieceType.Pawn;
        public override Player Color { get; }

        //Pawn
        // A pawn has a special property: it can move two squares forward on its first move.
        // A pawn can only move forward, and it captures diagonally ONLY if it captures.
        // This is represented by the HasMoved property.

        private readonly Direction forward;

        public Pawn(Player color)
        {
            Color = color;

            if(color == Player.White)
            {
                forward = Direction.North; // White pawns move up
            }
            else if(color == Player.Black)
            {
                forward = Direction.South; // Black pawns move down
            }
            else
            {
                throw new ArgumentException("Invalid player color for Pawn.");
            }
        }

        public override Piece Copy()
        {
            Pawn copy = new Pawn(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        private static bool CanMoveTo(Position position, Board board)
        {
            // Check if the position is inside the board and not occupied by a piece of the same color
            return Board.IsInside(position) && (board.IsEmpty(position));
        }

        private bool CanCapture(Position position, Board board)
        {
            // Check if the position is occupied by an opponent's piece
            if(!Board.IsInside(position) || board.IsEmpty(position))
            {
                return false;
            }

            return board[position].Color != Color;
        }

        private static IEnumerable<Move> PromotionMoves(Position from, Position to)
        {
            yield return new PawnPromotion(from, to, PieceType.Queen);
            yield return new PawnPromotion(from, to, PieceType.Rook);
            yield return new PawnPromotion(from, to, PieceType.Bishop);
            yield return new PawnPromotion(from, to, PieceType.Knight);
        }

        private IEnumerable<Move> ForwardMoves(Position from, Board board)
        {
            Position oneStepForward = from + forward;

            if(CanMoveTo(oneStepForward, board))
            {
                //MIGHT NEED TO CHANGE && COLOR
                if((oneStepForward.Row == 0) || (oneStepForward.Row == 7))
                {
                    // Promotion
                    foreach(Move promotion in PromotionMoves(from, oneStepForward))
                    {
                        yield return promotion;
                    }
                }
                else
                {
                    yield return new Normal(from, oneStepForward);
                }

                Position twoMovesForward = oneStepForward + forward;

                if(!HasMoved && CanMoveTo(twoMovesForward, board))
                {
                    yield return new DoublePawn(from, twoMovesForward);
                }
            }
        }

        private IEnumerable<Move> DiagonalMove(Position from, Board board)
        {
            foreach(Direction direction in new Direction[] {Direction.West, Direction.East })
            {
                Position diagonalPosition = from + forward + direction;

                if(diagonalPosition == board.GetPawnSkipPosition(Color.Opponent()))
                {
                    yield return new EnPassant(from, diagonalPosition);
                }else if (CanCapture(diagonalPosition, board))
                {
                    if ((diagonalPosition.Row == 0) || (diagonalPosition.Row == 7))
                    {
                        // Promotion
                        foreach (Move promotion in PromotionMoves(from, diagonalPosition))
                        {
                            yield return promotion;
                        }
                    }
                    else
                    {
                        yield return new Normal(from, diagonalPosition);
                    }
                }
            }
        }

        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return ForwardMoves(from, board)
                .Concat(DiagonalMove(from, board));
                //.Where(move => Board.IsInside(move.ToPos)); // Ensure all moves are within the board boundaries
        }

        public override bool CanCaptureOppKing(Position from, Board board)
        {
            //Check only if the king is in a diagonal position
            return DiagonalMove(from, board).Any(move =>
            {
                Piece piece = board[move.ToPos];
                return piece != null && piece.Type == PieceType.King;
            });
        }
    }
}
