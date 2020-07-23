using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace BackgammonEngine
{
    public class GameEngine
    {
        private const char WHITE_CHECKER = 'W';
        private const char BLACK_CHECKER = 'B';
        public GameState GameState { get; private set; }
        private enum MoveType { RegularMove, RessurectionMove, BearingOffMove, Undefined };

        public GameEngine(char gameStartingColor = 'W')
        {
            createInitialState(gameStartingColor);
        }

        private void createInitialState(char gameStartingColor)
        {
            GameState = new GameState
            {
                Triangles = new List<Stack<char>>(),
                EatenWhiteCheckers = new Stack<char>(),
                EatenBlackCheckers = new Stack<char>(),
                PlayingColor = 'N', // N stands for null
                RollingColor = gameStartingColor,
                RolledDices = new int[4]
            };

            for (int i = 0; i < 24; i++)
                GameState.Triangles.Add(new Stack<char>());
            placeCheckers();
        }

        private void placeCheckers()
        {
            for (int i = 0; i < 5; i++)
            {
                GameState.Triangles[11].Push(WHITE_CHECKER);
                GameState.Triangles[18].Push(WHITE_CHECKER);

                GameState.Triangles[5].Push(BLACK_CHECKER);
                GameState.Triangles[12].Push(BLACK_CHECKER);

                if (i < 3)

                {
                    GameState.Triangles[7].Push(BLACK_CHECKER);
                    GameState.Triangles[16].Push(WHITE_CHECKER);
                }
                if (i < 2)
                {
                    GameState.Triangles[23].Push(BLACK_CHECKER);
                    GameState.Triangles[0].Push(WHITE_CHECKER);
                }
            }
        }

        // if afterRoll is true then searching for valid move based on rolled dices,if false then searching for numbers from 1-6.
        private bool doesValidMoveExist(char checkerColor, bool afterRollCheck)
        {
            // if on bearing-off stage there will be always availble move
            if (doesOnBearingOff(checkerColor))
                return true;

            int[] dicesToCheck = afterRollCheck ?
                (GameState.RolledDices[0] == GameState.RolledDices[1] ? new int[] { GameState.RolledDices[0] } :
                new int[] { GameState.RolledDices[0], GameState.RolledDices[1] }) : new int[] { 1, 2, 3, 4, 5, 6 };

            Stack<char> eatenCheckers = checkerColor == WHITE_CHECKER ? GameState.EatenWhiteCheckers : GameState.EatenBlackCheckers;

            // in case there is eaten checker
            if (eatenCheckers.Count > 0)
            {
                int[] trianglesToCheck = checkerColor == WHITE_CHECKER ? new int[] { 0, 1, 2, 3, 4, 5 } : new int[] { 23, 22, 21, 20, 19, 18 };
                if (afterRollCheck)
                {
                    for (int i = 0; i < dicesToCheck.Length; i++)
                        if (dicesToCheck[i] != 0 && canCheckerBePlacedWithinTriangle(checkerColor, trianglesToCheck[dicesToCheck[i] - 1]))
                            return true;
                }
                else
                    for (int i = 0; i < trianglesToCheck.Length; i++)
                        if (canCheckerBePlacedWithinTriangle(checkerColor, trianglesToCheck[i]))
                            return true;
            }
            else
            {   // reversing move direction for black checkers
                if (checkerColor == 'B')
                    for (int i = 0; i < dicesToCheck.Length; i++)
                        dicesToCheck[i] *= -1;

                for (int i = 0; i < GameState.Triangles.Count; i++)
                {
                    if (GameState.Triangles[i].Count > 0 && GameState.Triangles[i].Peek() == checkerColor)
                    {
                        for (int k = 0; k < dicesToCheck.Length; k++)
                            if (dicesToCheck[k] != 0 && canCheckerBePlacedWithinTriangle(checkerColor, i + dicesToCheck[k]))
                                return true;
                    }
                }
            }
            GameState.InfoMessage = checkerColor + " you got no available move";
            return false;
        }

        private bool canCheckerBePlacedWithinTriangle(char checkerColor, int triangleIndex)
        {
            if (triangleIndex < 0 || triangleIndex > 23)
                return false;

            Stack<char> triangle = GameState.Triangles[triangleIndex];
            if (triangle.Count < 2)
                return true;
            if (triangle.Peek() == checkerColor)
                return true;

            return false;
        }

        private bool doesOnBearingOff(char checkerColor)
        {
            Stack<char> eatenCheckers = checkerColor == WHITE_CHECKER ? GameState.EatenWhiteCheckers : GameState.EatenBlackCheckers;
            if (eatenCheckers.Count > 0)
                return false;

            int[] indexesSpanToCheck = checkerColor == WHITE_CHECKER ? new int[] { 0, 18 } : new int[] { 6, 24 };
            for (int i = indexesSpanToCheck[0]; i < indexesSpanToCheck[1]; i++)
                if (GameState.Triangles[i].Count > 0 && GameState.Triangles[i].Peek() == checkerColor)
                    return false;

            return true;
        }

        private void resetDice(bool resetAll, int diceToReset = 0)
        {
            if (resetAll)
            {
                for (int i = 0; i < GameState.RolledDices.Length; i++)
                    GameState.RolledDices[i] = 0;
                return;
            }

            for (int i = 3; i >= 0; i--)
            {
                if (GameState.RolledDices[i] == diceToReset)
                {
                    GameState.RolledDices[i] = 0;
                    return;
                }
            }
        }

        private void tryToSwitchRoles()
        {
            // trying to swtich roles after all dices were moved.
            if (GameState.RolledDices[0] == 0 && GameState.RolledDices[1] == 0)
            {
                char opponentColor = GameState.PlayingColor == WHITE_CHECKER ? BLACK_CHECKER : WHITE_CHECKER;
                if (doesValidMoveExist(opponentColor, false))
                    GameState.RollingColor = opponentColor;
                else
                    GameState.RollingColor = GameState.PlayingColor;

                GameState.PlayingColor = 'N';
            }
            else
            {   // trying to switch roles right after dices were rolled.
                if (GameState.PlayingColor == 'N')
                {
                    if (doesValidMoveExist(GameState.RollingColor, true))
                    {
                        GameState.PlayingColor = GameState.RollingColor;
                        GameState.RollingColor = 'N';
                    }
                    else
                    {
                        GameState.InfoMessage = String.Format("{0} you have no available move with [{1},{2}] dices", GameState.RollingColor, GameState.RolledDices[0], GameState.RolledDices[1]);
                        GameState.RollingColor = GameState.RollingColor == WHITE_CHECKER ? BLACK_CHECKER : WHITE_CHECKER;
                        resetDice(true);
                    }

                } // trying to switch roles after move was made but there are one or more dices left.
                else if (!doesValidMoveExist(GameState.PlayingColor, true))
                {
                    GameState.RollingColor = GameState.PlayingColor == WHITE_CHECKER ? BLACK_CHECKER : WHITE_CHECKER;
                    GameState.PlayingColor = 'N';
                    resetDice(true);
                }
            }
        }

        private bool isRespectiveDiceExist(MoveType moveType, out int diceNumber, params int[] moveIndexes)
        {
            char checkerColor = GameState.PlayingColor;

            // regular move
            if (moveType == MoveType.RegularMove)
            {
                int moveSize = Math.Abs(moveIndexes[1] - moveIndexes[0]);
                for (int i = 0; i < 2; i++)
                {
                    if (moveSize == GameState.RolledDices[i])
                    {
                        diceNumber = moveSize;
                        return true;
                    }
                }
            }
            // resurrection move or bearing-off move
            else if (moveType == MoveType.RessurectionMove || moveType == MoveType.BearingOffMove)
            {
                int[] targetedIndexes;

                if (moveType == MoveType.RessurectionMove)
                    targetedIndexes = checkerColor == WHITE_CHECKER ? new int[] { 0, 1, 2, 3, 4, 5 } : new int[] { 23, 22, 21, 20, 19, 18 };
                else
                    targetedIndexes = checkerColor == WHITE_CHECKER ? new int[] { 23, 22, 21, 20, 19, 18 } : new int[] { 0, 1, 2, 3, 4, 5 };

                for (int i = 0; i < targetedIndexes.Length; i++)
                {
                    if (targetedIndexes[i] == moveIndexes[0])
                    {
                        for (int k = 0; k < 2; k++)
                        {
                            if (GameState.RolledDices[k] == i + 1)
                            {
                                diceNumber = i + 1;
                                return true;
                            }
                        }
                    }
                }
            }
            diceNumber = 0;
            return false;
        }

        private MoveType determineMoveType(int indexFrom, int indexTo)
        {
            MoveType moveType;

            if (indexTo >= 0 && indexFrom >= 0)
                moveType = MoveType.RegularMove;
            else if (indexFrom < 0 && indexTo >= 0)
                moveType = MoveType.RessurectionMove;
            else if (indexFrom >= 0 && indexTo < 0)
                moveType = MoveType.BearingOffMove;
            else
                moveType = MoveType.Undefined;

            return moveType;
        }

        private bool tryToSetAWinner()
        {
            for (int i = 0; i < GameState.Triangles.Count; i++)
                if (GameState.Triangles[i].Count > 0 && GameState.Triangles[i].Peek() == GameState.PlayingColor)
                    return false; ;

            GameState.WinnerColor = GameState.PlayingColor;
            return true;
        }

        private void makeAMove(MoveType moveType, params int[] moveIndexes)
        {
            if (moveType == MoveType.BearingOffMove)
            {
                GameState.Triangles[moveIndexes[0]].Pop();
                if (tryToSetAWinner())
                {
                    GameState.InfoMessage = String.Format("{0} YOU ARE A WINNER !!!", GameState.PlayingColor);
                    return;
                }

                tryToSwitchRoles();
                return;
            }
            // eating condition
            int indexTo = moveType == MoveType.RegularMove ? moveIndexes[1] : moveIndexes[0];
            if (GameState.Triangles[indexTo].Count == 1 && GameState.Triangles[indexTo].Peek() != GameState.PlayingColor)
            {
                Stack<char> opponentsEatenCheckers = GameState.PlayingColor == WHITE_CHECKER ? GameState.EatenBlackCheckers : GameState.EatenWhiteCheckers;
                opponentsEatenCheckers.Push(GameState.Triangles[indexTo].Pop());
            }

            if (moveType == MoveType.RegularMove)
                GameState.Triangles[moveIndexes[1]].Push(GameState.Triangles[moveIndexes[0]].Pop());

            else if (moveType == MoveType.RessurectionMove)
            {
                Stack<char> currentlyPlayingEatenCheckers = GameState.PlayingColor == WHITE_CHECKER ? GameState.EatenWhiteCheckers : GameState.EatenBlackCheckers;
                GameState.Triangles[moveIndexes[0]].Push(currentlyPlayingEatenCheckers.Pop());
            }
            tryToSwitchRoles();
        }

        public void RollDices()
        {
            GameState.InfoMessage = null; // deleting info message with each new turn.

            Random random = new Random();
            for (int i = 0; i < 2; i++)
                GameState.RolledDices[i] = random.Next(1, 7);

            if (GameState.RolledDices[0] == GameState.RolledDices[1])
            {
                GameState.RolledDices[2] = GameState.RolledDices[0];
                GameState.RolledDices[3] = GameState.RolledDices[0];
            }
            tryToSwitchRoles();
        }

        public bool TryToMove(int indexFrom, int indexTo)
        {
            if (GameState.PlayingColor == 'N' || indexFrom == indexTo)
                return false;

            MoveType moveType = determineMoveType(indexFrom, indexTo);
            Stack<char> eatenCheckers = GameState.PlayingColor == WHITE_CHECKER ? GameState.EatenWhiteCheckers : GameState.EatenBlackCheckers;
            int diceNumber;

            // regular move
            if (moveType == MoveType.RegularMove && eatenCheckers.Count == 0)
            {
                int moveDirection = indexTo - indexFrom;
                if ((moveDirection > 0 && GameState.PlayingColor == BLACK_CHECKER) || (moveDirection < 0 && GameState.PlayingColor == WHITE_CHECKER))
                    return false;
                if (!isRespectiveDiceExist(MoveType.RegularMove, out diceNumber, indexFrom, indexTo))
                    return false;
                if (GameState.Triangles[indexFrom].Count == 0 ||
                    GameState.Triangles[indexFrom].Peek() != GameState.PlayingColor ||
                    !canCheckerBePlacedWithinTriangle(GameState.PlayingColor, indexTo))
                    return false;
                resetDice(false, diceNumber);
                makeAMove(MoveType.RegularMove, indexFrom, indexTo);
                return true;
            }
            // resurrection move
            if (moveType == MoveType.RessurectionMove && eatenCheckers.Count > 0)
            {
                int[] homeIndexesSpan = GameState.PlayingColor == WHITE_CHECKER ? new int[] { 0, 5 } : new int[] { 18, 23 };
                if (!(indexTo >= homeIndexesSpan[0] && indexTo <= homeIndexesSpan[1]) || !canCheckerBePlacedWithinTriangle(GameState.PlayingColor, indexTo))
                    return false;
                if (!isRespectiveDiceExist(MoveType.RessurectionMove, out diceNumber, indexTo))
                    return false;
                resetDice(false, diceNumber);
                makeAMove(MoveType.RessurectionMove, indexTo);
                return true;
            }
            // bearing-off move
            if (moveType == MoveType.BearingOffMove && doesOnBearingOff(GameState.PlayingColor))
            {
                bool isLeftMostPopulatedTriangle = true;
                int searchDirection = GameState.PlayingColor == WHITE_CHECKER ? -1 : 1;
                int lastIndexToCheck = GameState.PlayingColor == WHITE_CHECKER ? 18 : 5;

                // checking if targeted index is the left most populated 
                for (int i = indexFrom + searchDirection; i != lastIndexToCheck + searchDirection; i += searchDirection)
                    if (GameState.Triangles[i].Count > 0 && GameState.Triangles[i].Peek() == GameState.PlayingColor)
                    {
                        isLeftMostPopulatedTriangle = false;
                        break;
                    }
                int[] targetedIndexesSpan = isLeftMostPopulatedTriangle ? new int[] { indexFrom, lastIndexToCheck } : new int[] { indexFrom, indexFrom };

                for (int i = targetedIndexesSpan[0]; i != targetedIndexesSpan[1] + searchDirection; i += searchDirection)
                    if (isRespectiveDiceExist(MoveType.BearingOffMove, out diceNumber, i))
                    {
                        resetDice(false, diceNumber);
                        makeAMove(MoveType.BearingOffMove, indexFrom);
                        return true;
                    }
                return false;
            }
            return false;
        }
    }
}
