using Courserio.Core.DTOs.Question;
using Courserio.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Courserio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet("{chapterId}")]
        public async Task<IActionResult> GetByChapterAsync(int chapterId)
        {
            var res = await _questionService.GetByChapterIdAsync(chapterId);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]QuestionCreateDto questionDto)
        {
            await _questionService.CreateAsync(questionDto);
            return Ok();
        }
    }
}
