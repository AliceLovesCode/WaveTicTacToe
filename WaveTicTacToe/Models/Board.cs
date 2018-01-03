using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WaveTicTacToe.Exceptions;

namespace WaveTicTacToe.Models
{
    public class Board
    {
        char[,] boardGrid = new char[3, 3];

        public Board(string board)
        {
            boardGrid = BoardGridFromString(board);
        }

        public string ResultBoard() {
            UpdateBoardGridWithMove();
            return StringFromBoardGrid(boardGrid);
        }

        private void UpdateBoardGridWithMove()
        {
            UpdateBoardGridWithNaiveMove();
        }

        private void UpdateBoardGridWithNaiveMove()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (boardGrid[i, j] == ' ')
                    {
                        boardGrid[i, j] = 'o';
                        return;
                    }
                }
            }
        }

        //this is not meant to be optimal, as I doubt I will have time to complete optimal play
        private void UpdateBoardGridWithBetterMove()
        {
            //first check if I can win; are there any rows, columns, or diagonals with two o's already?
            if (false)
            {

            }
            //second check if they are about to win; are there any rows, columns, or diagonals with two x's already?
            else if (false)
            {

            }
            //if the center is empty, play the center
            else if (false)
            {

            }
            //if the center isn't empty, play a corner in a column I already have an o in
            else if (false)
            {

            }
            //if I can't find anything else, play the naive move
            else
            {
                UpdateBoardGridWithNaiveMove();
            }
        }

        private static bool HasWinConditionBeenMet(char[,] board)
        {
            bool winConditionMet = false;

            for (int i = 0; i < 3; i++)
            {
                winConditionMet = AllOneChar(RowProjection(board, i), 'o') || AllOneChar(RowProjection(board, i), 'x');
                if (winConditionMet)
                {
                    return winConditionMet;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                winConditionMet = AllOneChar(ColumnProjection(board, i), 'o') || AllOneChar(ColumnProjection(board, i), 'x');
                if (winConditionMet)
                {
                    return winConditionMet;
                }
            }

            for (int i = 0; i < 2; i++)
            {
                winConditionMet = AllOneChar(DiagonalProjection(board, i), 'o') || AllOneChar(DiagonalProjection(board, i), 'x');
                if (winConditionMet)
                {
                    return winConditionMet;
                }
            }

            return winConditionMet;
        }
        private static bool AllOneChar(char[] input, char checkChar)
        {
            bool allOne = true;
            for (int j = 0; j < 3; j++)
            {
                if (input[j] != checkChar)
                {
                    allOne = false;
                }
            }

            return allOne;
        }

        private char[,] BoardGridFromString(string boardString)
        {
            if (!StringIsValidBoard(boardString))
            {
                throw new InvalidBoardException("Invalid board parameter");
            }
            if (!IsPlausibleInputBoard(boardString)) {
                throw new ImplausibleBoardException("Implausible board parameter");
            }
            else
            {
                var grid = new char[3, 3];
                int h = 0;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        grid[i, j] = boardString[h];
                        h++;
                    }
                }
                if (HasWinConditionBeenMet(grid))
                {
                    throw new ImplausibleBoardException("Game already won");
                }
                return grid;
            }
        }

        private string StringFromBoardGrid(char[,] grid)
        {
            StringBuilder boardStringBuilder = new StringBuilder();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    boardStringBuilder.Append(grid[i, j]);
                }
            }
            var boardString = boardStringBuilder.ToString();
            if (!IsPlausibleResultBoard(boardString))
            {
                throw new ImplausibleBoardException("Implausible result board");
            }
            return boardString;
        }

        private static bool IsPlausibleInputBoard(string board)
        {
            var xCount = board.Count(f => f == 'x');
            var oCount = board.Count(f => f == 'o');
            var spaceCount = board.Count(f => f == ' ');

            return oCount <= xCount && (xCount - oCount <= 1) && spaceCount > 0;
        }

        private static bool IsPlausibleResultBoard(string board)
        {
            var xCount = board.Count(f => f == 'x');
            var oCount = board.Count(f => f == 'o');

            return xCount <= oCount && (oCount - xCount <= 1);
        }

        private static bool StringIsValidBoard(string checkBoard)
        {
            if (checkBoard == null)
            {
                return false;
            }
            var checkValidChars = new Regex("^[ xo]{9}$");
            return checkValidChars.Match(checkBoard).Success;
        }

        //projection onto one dimensional arrays
        private static char[] RowProjection(char[,] grid, int rowNumber)
        {
            char[] row = new char[3];
            for (int i = 0; i < 3; i++)
            {
                row[i] = grid[rowNumber, i];
            }

            return row;
        }


        private static char[] ColumnProjection(char[,] grid, int colNumber)
        {
            char[] col = new char[3];
            for (int i = 0; i < 3; i++)
            {
                col[i] = grid[i, colNumber];
            }
            return col;
        }

        //diagonal 0 is the one starting at [0,0] and diagonal 1 is the one starting at [2, 0]
        private static char[] DiagonalProjection(char[,] grid, int diagonalNumber)
        {
            char[] diagonal = new char[3];
            if (diagonalNumber == 0)
            {
                diagonal[0] = grid[0, 0];
                diagonal[1] = grid[1, 1];
                diagonal[2] = grid[2, 2];
            }
            else
            {
                diagonal[0] = grid[2, 0];
                diagonal[1] = grid[1, 1];
                diagonal[2] = grid[0, 2];
            }
            return diagonal;
        }

        private static int ProjectionCharacterCount(char[] projection, char checkChar)
        {
            return projection.Count(f => f == checkChar);
        }
    }
}
