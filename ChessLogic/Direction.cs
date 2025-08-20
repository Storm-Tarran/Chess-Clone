using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Direction
    {
        //Four cardinal directions
        public readonly static Direction North = new Direction(-1, 0);
        public static readonly Direction South = new Direction(1, 0);
        public static readonly Direction East = new Direction(0, 1);
        public static readonly Direction West = new Direction(0, -1);

        //Diagonal directions
        public static readonly Direction NorthEast = North + East;
        public static readonly Direction NorthWest = North + West;
        public static readonly Direction SouthEast = South + East;
        public static readonly Direction SouthWest = South + West;


        public int RowChange { get; }
        public int ColumnChange { get; }

        //Constructor to initialize the direction
        public Direction(int rowChange, int columnChange)
        {
            RowChange = rowChange;
            ColumnChange = columnChange;
        }

        //Override Equals method to compare two Direction objects
        public static Direction operator +(Direction left, Direction right)
        {
            
            return new Direction(left.RowChange + right.RowChange, left.ColumnChange + right.ColumnChange);
        }

        //May need to swap params
        public static Direction operator *(Direction direction, int multiplier)
        {
            return new Direction(direction.RowChange * multiplier, direction.ColumnChange * multiplier);
        }

    }
}
