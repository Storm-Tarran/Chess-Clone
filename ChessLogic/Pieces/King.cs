using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class King :Piece
    {
        public override PieceType Type => PieceType.King;
        public override Player Color { get; }

        private static readonly Direction[] directions = new Direction[]
        {
            Direction.North,
            Direction.East,
            Direction.South,
            Direction.West,
            Direction.NorthEast, 
            Direction.SouthEast,
            Direction.SouthWest, 
            Direction.NorthWest
        };
        public King(Player color)
        {
            Color = color;
        }

        private static bool IsUmovedRook(Position pos, Board board)
        {
            if(board.IsEmpty(pos))
            {
                return false;
            }
            Piece piece = board[pos];
            return piece.Type == PieceType.Rook && !piece.HasMoved;
        }

        private static bool AllEmpty(IEnumerable<Position> positions, Board board)
        {
            return positions.All(pos => board.IsEmpty(pos));
        }

        private bool CanCastleKingSide(Position from, Board board)
        {
            if(HasMoved)
            {
                return false;
            }

            Position rookPos = new Position(from.Row, 7);
            Position[] betweenPostitions = new Position[] { new(from.Row, 5), new(from.Row, 6) };

            return IsUmovedRook(rookPos, board) && AllEmpty(betweenPostitions, board);
        }

        private bool CanCastleQueenSide(Position from, Board board)
        {
            if (HasMoved)
            {
                return false;
            }
            Position rookPos = new Position(from.Row, 0);
            Position[] betweenPostitions = new Position[] { new(from.Row, 1), new(from.Row, 2), new(from.Row, 3) };

            return IsUmovedRook(rookPos, board) && AllEmpty(betweenPostitions, board);
        }

        public override Piece Copy()
        {
            King copy = new King(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        private IEnumerable<Position> MovePositions(Position from, Board board)
        {
            foreach(Direction direction in directions)
            {
                Position positionTo = from + direction;
                if(!Board.IsInside(positionTo))
                {
                    continue;
                }

                if(board.IsEmpty(positionTo) || board[positionTo].Color != Color)
                {
                    yield return positionTo;
                }
            }
        }

        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            foreach(Position positionTo in MovePositions(from, board))
            {
                yield return new Normal(from, positionTo);
            }

            if(CanCastleKingSide(from, board))
            {
                yield return new Castle(MoveType.CastlingKS, from);
            }

            if(CanCastleQueenSide(from, board))
            {
                yield return new Castle(MoveType.CastlingQS, from);
            }
        }

        public override bool CanCaptureOppKing(Position from, Board board)
        {
            //Add casting moves later
            return MovePositions(from, board).Any(to =>
            {
                Piece piece = board[to];
                return piece != null && piece.Type == PieceType.King;
            });
        }
    }
}
