using Courserio.Core.DTOs.Answer;
using Courserio.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Courserio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;

        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] AnswerCreateDto answerDto)
        {
            var res = await _answerService.CreateAsync(answerDto);
            return Ok();
        }
    }
}