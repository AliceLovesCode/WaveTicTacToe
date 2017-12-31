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
        char[,] inputBoardGrid = new char[3, 3];

        public string ResultBoard => StringFromBoardGrid(inputBoardGrid); 

        public Board(string board)
        {
            inputBoardGrid = BoardGridFromString(board);
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

            return oCount <= xCount && (xCount - oCount <= 1);
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
            var regex = new Regex("^[ xo]{9}$");
            return regex.Match(checkBoard).Success;
        }
    }
}
