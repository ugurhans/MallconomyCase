using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class LeaderboardsController : Controller
    {

        private readonly ILeaderBoardService _leaderBoardService;

        public LeaderboardsController(ILeaderBoardService leaderBoardService)
        {
            _leaderBoardService = leaderBoardService;
        }

        [HttpPost("CreateLeaderBoard")]
        public IActionResult CreateLeaderBoard()
        {
            var result = _leaderBoardService.CreateLeaderBoard();
            if (result.Success == true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetAllWithMonth")]
        public IActionResult GetAllWithMonthOrUserId(int month, string? userId)
        {
            var result = _leaderBoardService.GetAllWithMonthOrUserId(month, userId != null ? new ObjectId(userId) : null);
            if (result.Success == true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetUsersAward")]
        public IActionResult GetUsersAward(string userId)
        {
            ObjectId sUserId = new ObjectId(userId);
            var result = _leaderBoardService.GetUsersAward(sUserId);
            if (result.Success == true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}

