using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaveTicTacToe.Exceptions
{
    public class ImplausibleBoardException : Exception
    {
        public ImplausibleBoardException(string message) : base(message)
        {
        }
    }
}
