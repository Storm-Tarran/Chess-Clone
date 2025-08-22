using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public enum MoveType
    {
        Normal,         // A standard move
        Capture,        // A move that captures an opponent's piece
        EnPassant,      // A special pawn capture move
        CastlingKS,     // A special move involving the king and rook
        CastlingQS,     // A special move involving the king and rook on the queenside
        DoublePawnPush, // A pawn moving two squares forward from its starting position
        Promotion,      // A pawn reaching the opposite end of the board and being promoted
        Check,          // A move that puts the opponent's king in check
        Checkmate,      // A move that results in checkmate
        Stalemate       // A situation where no legal moves are available for the current player
    }
}
