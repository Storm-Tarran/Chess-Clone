using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Knight : Piece
    {
        public override PieceType Type => PieceType.Knight;
        public override Player Color { get; }

        public Knight(Player color)
        {
            Color = color;
        }
        public override Piece Copy()
        {
            Knight copy = new Knight(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        private static IEnumerable<Position> PotenitialPositions(Position from)
        {
            // The knight can move in an "L" shape: two squares in one direction and then one square perpendicular, or one square in one direction and then two squares perpendicular.
            foreach(Direction verticalDirection in new Direction[] {Direction.North, Direction.South})
            {
                foreach(Direction horizontalDirection in new Direction[] {Direction.West, Direction.East})
                {
                    yield return from + 2 * verticalDirection + horizontalDirection;
                    yield return from + 2 * horizontalDirection + verticalDirection;
                }
            }
        }

        private IEnumerable<Position> MovePositions(Position from, Board board)
        {
            return PotenitialPositions(from).Where(position => Board.IsInside(position) && (board.IsEmpty(position) || board[position].Color != Color));
        }

        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return MovePositions(from, board).Select(position => new Normal(from, position));
        }
    }
}
