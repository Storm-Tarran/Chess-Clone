using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public abstract class Piece
    {
        public abstract PieceType Type { get; }
        public abstract Player Color { get; }
        public bool HasMoved { get; set; } = false;

        public abstract Piece Copy();

        public abstract IEnumerable<Move> GetMoves(Position from, Board board);

        protected IEnumerable<Position> MovePositionInDirection(Position from, Board board, Direction direction)
        {
            for (Position position = from + direction; Board.IsInside(position); position += direction)
            {
                if (board.IsEmpty(position))
                {
                    yield return position; // If the square is empty, yield the position
                    continue;
                }
                //Piece piece = board[position];
                //if(piece.Color != Color)
                //{
                //    yield return position; // If the square has an opponent's piece, yield the position
                //}
                //yield break; // Stop if we hit a piece of the same color
                else
                {
                    if (board[position].Color != this.Color)
                    {
                        yield return position; // If the square has an opponent's piece, yield the position
                    }
                    break; // Stop if we hit a piece of the same color
                }
            }
        }

        protected IEnumerable<Position> MovePositionInDirections(Position from, Board board, Direction[] directions)
        {
            return directions.SelectMany(direction => MovePositionInDirection(from, board, direction));
        }
    }
}
