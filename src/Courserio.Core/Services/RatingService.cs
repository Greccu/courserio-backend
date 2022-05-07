using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Courserio.Core.DTOs.Rating;
using Courserio.Core.Interfaces.Repositories;
using Courserio.Core.Interfaces.Services;
using Courserio.Core.Middlewares.ExceptionMiddleware.CustomExceptions;
using Courserio.Core.Models;

namespace Courserio.Core.Services
{
    public class RatingService : IRatingService
    {
        private readonly IGenericRepository<Rating> _ratingRepository;
        private readonly IMapper _mapper;

        public RatingService(IGenericRepository<Rating> ratingRepository, IMapper mapper)
        {
            _ratingRepository = ratingRepository;
            _mapper = mapper;
        }


        public async Task CreateOrUpdateAsync(RatingCreateDto ratingDto)
        {
            if (ratingDto.Value is < 1 or > 5)
            {
                throw new CustomBadRequestException("Rating must be between 1 and 5");
            }
            var rating = _ratingRepository.AsQueryable().FirstOrDefault(r => r.UserId == ratingDto.UserId && r.CourseId == ratingDto.CourseId);
            if (rating is null)
            {
                rating = _mapper.Map<Rating>(ratingDto);
                
                await _ratingRepository.AddAsync(rating);
            }
            else
            {
                _mapper.Map(ratingDto, rating);
                rating.CreatedAt = DateTime.Now;
                await _ratingRepository.UpdateAsync(rating);
            }
        }

        
    }
}
