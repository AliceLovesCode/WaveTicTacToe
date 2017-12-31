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
        char[,] startingBoardGrid = new char[3, 3];
        public char[,] StartingBoardGrid { get; set; }

        public string ResultBoard => StringFromBoardGrid(startingBoardGrid); 

        public Board(string board)
        {
            startingBoardGrid = BoardGridFromString(board);
        }

        private char[,] BoardGridFromString(string boardString)
        {
            if (StringIsValidBoard(boardString))
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
            else
            {
                throw new InvalidBoardException("Invalid board parameter");
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

            return boardStringBuilder.ToString();
        }

        public bool IsPlausible()
        {
            return false;
        }

        public static bool StringIsValidBoard(string checkBoard)
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
