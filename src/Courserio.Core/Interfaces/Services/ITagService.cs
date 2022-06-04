using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courserio.Core.DTOs.Tags;

namespace Courserio.Core.Interfaces.Services
{
    public interface ITagService
    {
        Task<List<TagDto>> ListAsync();
        Task CreateManyAsync(List<string> tags);
        Task FollowAsync(int id, string username);
        Task UnfollowAsync(int id, string username);
    }
}
