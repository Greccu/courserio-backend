using System.Diagnostics;
using Courserio.Core.MachineLearningModel;
using Courserio.Core.MachineLearningModel.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Courserio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationController : ControllerBase
    {
        private readonly IModelService _modelService;

        public RecommendationController(IModelService modelService)
        {
            _modelService = modelService;
        }

        [HttpGet("train")]
        public async Task<IActionResult> TrainAsync()
        {
            await _modelService.TrainLastDayAsync();
            return Ok();
        }

        [HttpPost("predict")]
        public async Task<IActionResult> PredictAsync([FromBody] ModelInput input)
        {
            var result = _modelService.Predict(input);
            return Ok(result);
        }

        [HttpGet("load")]
        public async Task<IActionResult> LoadAsync()
        {
            await _modelService.LoadModelAsync();
            return Ok();
        }

        [HttpGet("test-predictions")]
        public async Task<IActionResult> TestPredictionsAsync()
        {
            var stopwatch = new Stopwatch();
            var courseList = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 ,11, 12,13,14,15,16,17,18,19,20};
            stopwatch.Start();
            for (var _ = 0; _ < 1000000; _++)
            {
                foreach (var course in courseList)
                {
                    _modelService.Predict(new ModelInput
                    {
                        CourseId = course,
                        UserId = 1
                    });
                }
            }
            stopwatch.Stop();
            Console.WriteLine($"{stopwatch.ElapsedMilliseconds} ms");
            return Ok($"{stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
