# Chess Clone (C#)
This is a simple chess game in C#. It models the full rules of chess (moves, checks, checkmates/ statlemates, promotions, castlling, en passant, etc) with a clear separation between board state, move generation, and game flow.

## Highlights
- Full legal move validation (can't leave king in check)
- Special moves: Castling, En Passant, Pawn Promotion, Double Pawn Push
- Check/ Checkmate / Stalemate / Insufficient Material / 50-Move Rule

## Tech Stack
- Language: C#
- Project Type: WPF

## To Run 
In terminal
- dotnet build
- dotnet run

## ♟️ Rules Implemented
- Piece movement + capture
- Check detection (opponent threats to your king)
- Legal move filtering (no move may leave your king in check)
- Castling (king cannot castle through/into check; rook/king unmoved)
- En Passant (only immediately after a double pawn push)
- Pawn Promotion
- Fifty-move rule
##Game termination:
- Checkmate (in-check & no legal moves)
- Stalemate (not in-check & no legal moves)
- Insufficient material / draw conditions

<img width="1008" height="1009" alt="image" src="https://github.com/user-attachments/assets/70f2797d-40f5-4055-a923-dfe68a6ad8e5" />
