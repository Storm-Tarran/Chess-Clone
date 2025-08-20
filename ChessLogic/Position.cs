using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Position
    {
        //Store the position of the piece on the board
        public int Row { get; }
        public int Column { get; }
        //Constructor to initialize the position
        public Position(int row, int cloumn)
        {
            Row = row;
            Column = cloumn;
        }
        
        public Player SquareColor()
        {
            if((Row + Column) % 2 == 0)
            {
                return Player.White; // Even sum means white square
            }
            else
            {
                return Player.Black; // Odd sum means black square
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Position position &&
                   Row == position.Row &&
                   Column == position.Column;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Column);
        }

        public static bool operator ==(Position left, Position right)
        {
            return EqualityComparer<Position>.Default.Equals(left, right);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !(left == right);
        }

        public static Position operator +(Position position, Direction direction)
        {
            return new Position(position.Row + direction.RowChange, position.Column + direction.ColumnChange);
        }
    }
}
