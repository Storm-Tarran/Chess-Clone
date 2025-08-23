using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public abstract class Move
    {
        //Based class for all the moves
        public abstract MoveType Type { get; }
        public abstract Position FromPos { get; }
        public abstract Position ToPos { get; }
        public abstract void Execute(Board board);

        //May change for computer players later (AI)
        public virtual bool IsLeagal(Board board)
        {
            Player player = board[FromPos].Color;
            Board copy = board.Copy();
            Execute(copy);
            return !board.IsInCheck(player);
        }
    }
}
