using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class GameState
    {
        public Board Board { get; }
        public Player CurrentPlayer { get; private set; }
        public Result Result { get; private set; } = null;

        private int noCaptureOrPawnMoves = 0;
        private string stateString;

        private readonly Dictionary<string, int> stateHistory = new Dictionary<string, int>();

        public GameState(Player player, Board board)
        {
            Board = board;
            CurrentPlayer = player;

            stateString = new StateString(CurrentPlayer, board).ToString();
            stateHistory[stateString] = 1;
        }

        public IEnumerable<Move> LegalMovesForPieces(Position pos)
        {
            if(Board.IsEmpty(pos) || Board[pos].Color != CurrentPlayer)
            {
                //No legal moves
                return Enumerable.Empty<Move>();
            }

            Piece piece = Board[pos];
            IEnumerable<Move> moveCandidates = piece.GetMoves(pos, Board);
            return moveCandidates.Where(move => move.IsLeagal(Board));
        }

        public void MakeMove(Move move)
        {
            Board.SetPawnSkipPosition(CurrentPlayer, null);
            bool captureOrPawn = move.Execute(Board);

            if(captureOrPawn)
            {
                noCaptureOrPawnMoves = 0;
                stateHistory.Clear();
            }
            else
            {
                noCaptureOrPawnMoves++;
            }

            CurrentPlayer = CurrentPlayer.Opponent();
            UpdateStateString();
            CheckForGameEnd();

        }

        public IEnumerable<Move> AllLegalMoves(Player player)
        {
            IEnumerable<Move> moveCandidates = Board.PiecePositionsFor(player).SelectMany(pos =>
            {
                Piece piece = Board[pos];
                return piece.GetMoves(pos, Board);
            });

            //Return only legal moves, filtering out illegal ones
            return moveCandidates.Where(move => move.IsLeagal(Board));
        }

        // PROBLEM IS HERE
        public void CheckForGameEnd()
        {
            if(!AllLegalMoves(CurrentPlayer).Any())
            {
                if(Board.IsInCheck(CurrentPlayer))
                {
                    Result = Result.Win(CurrentPlayer.Opponent());
                }
                else
                {
                    Result = Result.Draw(EndReason.Stalemate);
                }
            }
            else if(Board.InsufficientMaterial())
            {
                Result = Result.Draw(EndReason.InsufficientMaterial);
            }
            else if(FiftyMoveRule())
            {
                Result = Result.Draw(EndReason.DrawByFiftyMoveRule);
            }
            else if(ThreeFoldRepetition())
            {
                Result = Result.Draw(EndReason.ThreeFoldRepetition);
            }
        }

        public bool IsGameOver()
        {
            return Result != null;
        }

        private bool FiftyMoveRule()
        {
            int fullMoves = noCaptureOrPawnMoves / 2;
            return fullMoves == 50;
        }

        private void UpdateStateString()
        {
            stateString = new StateString(CurrentPlayer, Board).ToString();

            if(!stateHistory.ContainsKey(stateString))
            {
                stateHistory[stateString] = 1;
            }
            else
            {
                stateHistory[stateString]++;
            }
        }

        private bool ThreeFoldRepetition()
        {
            return stateHistory[stateString] == 3;
        }
    }
}
