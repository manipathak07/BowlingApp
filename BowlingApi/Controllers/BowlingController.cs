using BowlingApp.Domain.Interfaces;
using BowlingApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BowlingApp.Controllers
{
    [Route("api/bowling")]
    [ApiController]
    public class BowlingController : ControllerBase
    {
        private readonly IBowlingGameFactory _game;

        public BowlingController(IBowlingGameFactory game)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
        }

        [HttpPost("play")]
        [Produces("application/xml", "application/json")]
        [Consumes("application/json")]
        public IActionResult Play([FromBody] RollInputModel input)
        {

            bool useTwelvePins = input.UseTwelvePins;
            if (input == null || input.Rolls == null || input.Rolls.Length != 21)
            {
                return BadRequest("Invalid input. Please provide an array of 21 integers.");
            }

            if (!useTwelvePins && input.Rolls.Any(pins => pins < 0 || pins > 10))
            {
                return BadRequest("Invalid number of pins.");
            }
            else if (useTwelvePins && input.Rolls.Any(pins => pins < 0 || pins > 12))
            {
                return BadRequest("Invalid number of pins.");
            }
            //Factory Implementation
            //Creating the instance of subclasses at runtime .
            IBowlingGame game = useTwelvePins ? _game.CreateTwelvePinBowlingGame() : _game.CreateStandardBowlingGame();
            // Add rolls
            foreach (var pins in input.Rolls)
            {
                game.Roll(pins);
            }

            // Check if the request also wants to retrieve the score
            if (input.RetrieveScore)
            {
                try
                {
                    int totalScore = game.GetTotalScore();
                    return Ok(totalScore);
                }
                catch (Exception ex)
                {
                    // Log the exception, and return an internal server error
                    return StatusCode(500, "An error occurred while calculating the score.");
                }
            }

            // If not retrieving the score immediately, return a success response
            return Ok();
       
        }

  
    }
}
