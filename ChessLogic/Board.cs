using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Board
    {
        private readonly Piece[,] pieces = new Piece[8, 8];

        //En Passante moves
        private readonly Dictionary<Player, Position> pawnSkipPositions = new Dictionary<Player, Position>
        {
            {Player.White, null },
            {Player.Black, null },
        };

        public Piece this[int row, int col]
        {
            get { return pieces[row, col]; }
            set { pieces[row, col] = value;}
        }

        public Piece this[Position position]
        {
            get { return this[position.Row, position.Column]; }
            set { this[position.Row, position.Column] = value; }
        }

        public Position GetPawnSkipPosition(Player player)
        {
            return pawnSkipPositions[player];
        }

        public void SetPawnSkipPosition(Player player, Position pos)
        {
            pawnSkipPositions[player] = pos;
        }

        public static Board Initial()
        {
            Board board = new Board();
            board.AddStartPieces();
            return board;
        }

        private void AddStartPieces()
        {
            //Black Pieces
            this[0, 0] = new Rook(Player.Black);
            this[0, 1] = new Knight(Player.Black);
            this[0, 2] = new Bishop(Player.Black);
            this[0, 3] = new Queen(Player.Black);
            this[0, 4] = new King(Player.Black);
            this[0, 5] = new Bishop(Player.Black);
            this[0, 6] = new Knight(Player.Black);
            this[0, 7] = new Rook(Player.Black);

            //White Pieces
            this[7, 0] = new Rook(Player.White);
            this[7, 1] = new Knight(Player.White);
            this[7, 2] = new Bishop(Player.White);
            this[7, 3] = new Queen(Player.White);
            this[7, 4] = new King(Player.White);
            this[7, 5] = new Bishop(Player.White);
            this[7, 6] = new Knight(Player.White);
            this[7, 7] = new Rook(Player.White);

            for(int i = 0; i < 8; i++)
            {
                this[1, i] = new Pawn(Player.Black);
                this[6, i] = new Pawn(Player.White);
            }
        }

        public static bool IsInside(Position position)
        {
            return position.Row >= 0 && position.Row < 8 && position.Column >= 0 && position.Column < 8;
        }

        public bool IsEmpty(Position position)
        {
            //if (!IsInside(position))
            //    throw new ArgumentOutOfRangeException(nameof(position), "Position is outside the board boundaries.");
            return this[position] == null;
        }

        public IEnumerable<Position> PiecePositions()
        {
            for(int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Position position = new Position(row, col);

                    if (!IsEmpty(position))
                    {
                        yield return position;
                    }
                }
            }
        }

        public IEnumerable<Position> PiecePositionsFor(Player player)
        {
            return PiecePositions().Where(pos => this[pos].Color == player);
        }

        //CHECK METHOD
        public bool IsInCheck(Player player)
        {
            return PiecePositionsFor(player.Opponent()).Any(pos =>
            {
                Piece piece = this[pos];
                return piece.CanCaptureOppKing(pos, this);
            });
        }

        //copy board
        public Board Copy()
        {
            Board copy = new Board();
            foreach(Position pos in PiecePositions())
            {
                copy[pos] = this[pos].Copy();
            }

            return copy;
        }
    }
}
