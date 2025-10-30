using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WaterBucketChallengeApi.Interfaces;
using WaterBucketChallengeApi.Models.Dtos;
using WaterBucketChallengeApi.Services;

namespace WaterBucketChallengeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaterBucketController : ControllerBase
    {
        private readonly IWaterBucketService _wjService;

        public WaterBucketController(IWaterBucketService wjService)
        {
            _wjService = wjService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Solve([FromBody] WaterBucketRequestDto request)
        {
            var solution = _wjService.Solve(
                request.x_capacity,
                request.y_capacity,
                request.z_amount_wanted
            );

            if (solution != null)
            {
                return Ok(new { solution = solution });
            }
            else
            {
                return BadRequest(new
                {
                    solution = "No Solution"
                });
            }
        }
    }
}
