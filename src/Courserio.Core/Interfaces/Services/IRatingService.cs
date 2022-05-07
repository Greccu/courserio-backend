using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courserio.Core.DTOs.Rating;

namespace Courserio.Core.Interfaces.Services
{
    public interface IRatingService
    {
        Task CreateOrUpdateAsync(RatingCreateDto ratingDto);
    }
}
