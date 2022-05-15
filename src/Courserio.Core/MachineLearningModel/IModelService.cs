using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courserio.Core.MachineLearningModel.Entities;

namespace Courserio.Core.MachineLearningModel
{
    public interface IModelService
    {
        Task LoadModelAsync();
        Task TrainLastDayAsync();
        ModelOutput Predict(ModelInput input);
    }
}
