using Courserio.Core.MachineLearningModel;
using Hangfire;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Courserio.Api
{
    public class HangfireSetup
    {
        public static void AddJobs()
        {
            BackgroundJob.Enqueue<IModelService>((modelService) => modelService.LoadModelAsync());
            RecurringJob.AddOrUpdate<IModelService>("train-recommendation-engine", (modelService) => modelService.TrainLastDayAsync(), Cron.Daily);
        }
    }
}
