using Courserio.Core.DTOs.Rating;
using Courserio.Core.Interfaces.Services;
using Courserio.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Courserio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateAsync(RatingCreateDto ratingDto)
        {
            await _ratingService.CreateOrUpdateAsync(ratingDto);
            return Ok();
        }
        
        [HttpGet("update-ratings/{id}")]
        public async Task<IActionResult> UpdateRatingsAsync(int id)
        {
            await _ratingService.UpdateRatingForOneAsync(id);
            return Ok();
        }
    }
}
