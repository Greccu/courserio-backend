using Courserio.Core.Enums;
using Courserio.Core.Interfaces.Repositories;
using Courserio.Core.MachineLearningModel.Entities;
using Courserio.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;

namespace Courserio.Core.MachineLearningModel
{
    public class ModelService : IModelService
    {
        private readonly MLContext _mlContext; 
        private PredictionEngine<ModelInput, ModelOutput> _model = null;
        private readonly IGenericRepository<MlModel> _mlModelRepository;
        private readonly IGenericRepository<Rating> _ratingRepository;

        public ModelService(IGenericRepository<MlModel> mlModelRepository, IGenericRepository<Rating> ratingRepository)
        {
            _mlModelRepository = mlModelRepository;
            _ratingRepository = ratingRepository;
            _mlContext = new MLContext();
        }

        public async Task LoadModelAsync()
        {
            //var mlContext = new MLContext();
            await using Stream stream = new MemoryStream();
            var mlModel = _mlContext.Model.Load(stream, out var _);
            _model = _mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
            
        }
        public async Task TrainLastDayAsync()
        {
            //var input = new List<ModelInput>();

            var input = await _ratingRepository.AsQueryable()
                .Where(x => x.CreatedAt.Date == DateTime.Now.Date.AddDays(-1))
                .Include(x => x.Course)
                .Select(x => new ModelInput
            {
                CourseId = x.CourseId,
                UserId = x.UserId,
                UserRating = x.Value,
                Rating = x.Course.AverageRating, 
                RatingCount = x.Course.RatingsCount
                }).ToListAsync();

            var trainData = _mlContext.Data.LoadFromEnumerable(input);
            
            var pipeline = BuildPipeline(_mlContext);
            
            var model = pipeline.Fit(trainData);
            var stream = new MemoryStream();
            _mlContext.Model.Save(model, trainData.Schema, stream);
            var bytes = stream.ToArray();
            await _mlModelRepository.AddAsync(new MlModel
            {
                Type = MlModelTypeEnum.CourseRecommendation,
                Content = bytes
            });
        }

        public ModelOutput Predict(ModelInput input)
        {
            return _model.Predict(input);
        }

        public static IEstimator<ITransformer> BuildPipeline(MLContext mlContext)
        {
            // Data process configuration with pipeline data transformations
            var pipeline = mlContext.Transforms.Conversion.MapValueToKey(@"userId", @"userId")
                .Append(mlContext.Transforms.Conversion.MapValueToKey(@"courseId", @"courseId"))
                .Append(mlContext.Recommendation().Trainers.MatrixFactorization(labelColumnName: @"user-rating", matrixColumnIndexColumnName: @"userId", matrixRowIndexColumnName: @"courseId"));

            return pipeline;
        }


    }
}
