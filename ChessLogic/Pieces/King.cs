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
        }
    }
}
