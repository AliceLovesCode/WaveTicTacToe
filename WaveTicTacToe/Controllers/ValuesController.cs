using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WaveTicTacToe.Models;
using WaveTicTacToe.Exceptions;

namespace WaveTicTacToe.Controllers
{
    [Route("[controller]")]
    public class PlayController : Controller
    {
        // GET /play
        [HttpGet]
        public IActionResult Get()
        {
            //check for valid input (the only valid input is a 9-character long string in the `board` parameter containing spaces, 'x' and 'o' characters.
            //invalid input is a bad request
            Board board;
            try
            {
                board = new Board(Request.Query["board"]);
            }
            catch (InvalidBoardException ex)
            {
                return BadRequest(ex.Message);
            }


            //check that it is plausibly o's turn; this is the case if there are equal or fewer o's than x's on the board
            //not plausibly o's turn is a bad request

            //find a move and return the board position with that move made

            //check  that the board we constructed is valid
            //server error if not (we messed up somehow)

            //check that it is now plausibly x's turn; this is the case if there are equal or fewer x's than o's on the board
            //server error if not (we messed up somehow)
            return Ok(board.ResultBoard);
        }
    }
}
