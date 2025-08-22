using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Normal : Move
    {
        //Used for all moves just for 1 position to another
        public override MoveType Type => MoveType.Normal;
        public override Position FromPos { get; }
        public override Position ToPos { get; }

        public Normal(Position from, Position to)
        {
            FromPos = from;
            ToPos = to;
        }

        public override void Execute(Board board)
        {
            //Move the piece from one position to another
            Piece piece = board[FromPos];
            board[ToPos] = piece;
            board[FromPos] = null; // Clear the original position
            piece.HasMoved = true; // Mark the piece as moved
        }
    }
}
