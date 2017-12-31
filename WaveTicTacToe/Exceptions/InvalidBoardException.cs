using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaveTicTacToe.Exceptions
{
    public class InvalidBoardException : Exception
    {
        public InvalidBoardException(string message) : base(message)
        {
        }
    }
}
