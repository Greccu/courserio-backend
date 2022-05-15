using Courserio.Core.Enums;
using Courserio.Core.Interfaces.Repositories;
using Courserio.Core.MachineLearningModel.Entities;
using Courserio.Core.Middlewares.ExceptionMiddleware.CustomExceptions;
using Courserio.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;

namespace Courserio.Core.MachineLearningModel
{
    public class ModelService : IModelService
    {
        private static readonly MLContext MlContext = new MLContext();
        private static PredictionEngine<ModelInput, ModelOutput> _model = null;
        private readonly IGenericRepository<MlModel> _mlModelRepository;
        private readonly IGenericRepository<Rating> _ratingRepository;

        public ModelService(
            IGenericRepository<MlModel> mlModelRepository,
            IGenericRepository<Rating> ratingRepository)
        {
            _mlModelRepository = mlModelRepository;
            _ratingRepository = ratingRepository;
        }

        public async Task LoadModelAsync()
        {
            //var mlContext = new MLContext();
            var model = await _mlModelRepository
                .AsQueryable()
                .Where(x => x.Type == MlModelTypeEnum.CourseRecommendation)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync();
            if (model is null)
            {
                throw new CustomNotFoundException("No model found");
            }
            var stream = new MemoryStream(model.Content);
            var mlModel = MlContext.Model.Load(stream, out var _);
            _model = MlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
            
        }
        public async Task TrainLastDayAsync()
        {
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

            
            var pipeline = BuildPipeline(MlContext);
            
            var trainData = MlContext.Data.LoadFromEnumerable(input);

            var model = pipeline.Fit(trainData);
            var stream = new MemoryStream();
            MlContext.Model.Save(model, trainData.Schema, stream);
            var bytes = stream.ToArray();
            await _mlModelRepository.AddAsync(new MlModel
            {
                Type = MlModelTypeEnum.CourseRecommendation,
                Content = bytes
            });
        }

        public ModelOutput Predict(ModelInput input)
        {
            if (_model is null)
            {
                throw new CustomBadRequestException("Recommendation Model Unavailable!");
            }
            return _model.Predict(input);
        }

        private static IEstimator<ITransformer> BuildPipeline(MLContext mlContext)
        {
            // Data process configuration with pipeline data transformations
            var pipeline = mlContext.Transforms.Conversion.MapValueToKey(@"userId", @"userId")
                .Append(mlContext.Transforms.Conversion.MapValueToKey(@"courseId", @"courseId"))
                .Append(mlContext.Recommendation().Trainers.MatrixFactorization(labelColumnName: @"user-rating", matrixColumnIndexColumnName: @"userId", matrixRowIndexColumnName: @"courseId"));

            return pipeline;
        }


    }
}
