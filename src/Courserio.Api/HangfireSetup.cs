using Courserio.Core.MachineLearningModel;
using Hangfire;

namespace Courserio.Api
{
    public class HangfireSetup
    {
        public void Setup()
        {
            //RecurringJob(() => { });
            //RecurringJob.AddOrUpdate("train-recommendation-engine",
            //    (IModelService modelService) => { modelService.TrainLastDayAsync();  }, Cron.Daily);
            
            RecurringJob.AddOrUpdate("train-recommendation-engine", (IModelService modelService) => modelService.TrainLastDayAsync(), Cron.Daily);
        }
    }
}
