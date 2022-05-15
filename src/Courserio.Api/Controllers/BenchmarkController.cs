using System.Diagnostics;
using Courserio.Core.Interfaces.Services;
using Courserio.Core.MachineLearningModel;
using Courserio.Core.MachineLearningModel.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Courserio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BenchmarkController : ControllerBase
    {
        [HttpGet("recommendation")]
        public async Task<IActionResult> BenchmarkRecommendationAsync([FromServices] ICourseService courseService)
        {
            const int numberOfIterations = 100;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var username = "elgreco";
            for (var i = 0; i < numberOfIterations; i++)
            {
                var courses = await courseService.GetRecommendedAsync(username);
            }
            stopwatch.Stop();
            var message = $"{numberOfIterations} iterations of recommendation took {stopwatch.ElapsedMilliseconds} ms";
            Console.WriteLine(message);
            return Ok(message);
        }

        [HttpGet("predictions")]
        public IActionResult BenchmarkPredictions([FromServices] IModelService modelService)
        {
            const int numberOfIterations = 1000000;
            var stopwatch = new Stopwatch();
            var courseList = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
            stopwatch.Start();
            for (var _ = 0; _ < numberOfIterations; _++)
            {
                foreach (var course in courseList)
                {
                    modelService.Predict(new ModelInput
                    {
                        CourseId = course,
                        UserId = 1
                    });
                }
            }
            stopwatch.Stop();
            var message = $"{numberOfIterations} iterations of {courseList.Length} predictions took {stopwatch.ElapsedMilliseconds} ms";
            Console.WriteLine(message);
            return Ok(message);
        }
    }
}
