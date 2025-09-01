using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Castle : Move
    {
        public override MoveType Type { get; }
        public override Position FromPos { get; }
        public override Position ToPos { get; }

        private readonly Direction kingMoveDirection;
        private readonly Position rookFromPostion;
        private readonly Position rookToPostion;

        public Castle(MoveType type, Position kingPos)
        {
            Type = type;
            FromPos = kingPos;

            if(type == MoveType.CastlingKS)
            {
                kingMoveDirection = Direction.East;
                ToPos = new Position(kingPos.Row, 6);
                rookFromPostion = new Position(kingPos.Row, 7);
                rookToPostion = new Position(kingPos.Row, 5);
            }
            else if(type == MoveType.CastlingQS)
            {
                kingMoveDirection = Direction.West;
                ToPos = new Position(kingPos.Row, 2);
                rookFromPostion = new Position(kingPos.Row, 0);
                rookToPostion = new Position(kingPos.Row, 3);
            }
        }

        public override void Execute(Board board)
        {
            new Normal(FromPos, ToPos).Execute(board);
            new Normal(rookFromPostion, rookToPostion).Execute(board);
        }

        public override bool IsLeagal(Board board)
        {
            Player player = board[FromPos].Color;

            if (board.IsInCheck(player))
            {
                return false;
            }

            Board copy = board.Copy();
            Position kingPosInCopy = FromPos;

            for(int i = 0; i < 2; i++)
            {
                new Normal(kingPosInCopy, kingPosInCopy + kingMoveDirection).Execute(copy);
                kingPosInCopy += kingMoveDirection;

                if(copy.IsInCheck(player))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
