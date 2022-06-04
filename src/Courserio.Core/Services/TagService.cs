using AutoMapper;
using Courserio.Core.DTOs.Tags;
using Courserio.Core.Interfaces.Repositories;
using Courserio.Core.Interfaces.Services;
using Courserio.Core.Middlewares.ExceptionMiddleware.CustomExceptions;
using Courserio.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Courserio.Core.Services
{
    public class TagService : ITagService
    {
        private readonly IGenericRepository<Tag> _tagRepository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public TagService(IGenericRepository<Tag> tagRepository, IMapper mapper, IGenericRepository<User> userRepository)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<List<TagDto>> ListAsync()
        {
            var tags = await _tagRepository
                .AsQueryable()
                .OrderBy(t => t.Name)
                .ToListAsync();
            return tags.Select(_mapper.Map<TagDto>).ToList();
        }

        public async Task CreateManyAsync(List<string> tags)
        {
            var tagsToAdd = tags.Select(_mapper.Map<Tag>).ToList();
            await _tagRepository.AddRangeAsync(tagsToAdd);
        }

        public async Task FollowAsync(int id, string username)
        {
            var user = await _userRepository
                .AsQueryable()
                .Where(x => x.Username.Equals(username))
                .Include(x => x.Tags)
                .FirstOrDefaultAsync();
            if (user is null)
                throw new CustomNotFoundException("User not found");
            var tag = await _tagRepository.GetByIdAsync(id);
            if (tag is null)
                throw new CustomNotFoundException("Tag not found");
            if(user.Tags.Contains(tag))
                throw new CustomBadRequestException("User already follows this tag");
            user.Tags.Add(tag);
            await _userRepository.SaveChangesAsync();
        }

        public async Task UnfollowAsync(int id, string username)
        {
            var user = await _userRepository
                .AsQueryable()
                .Where(x => x.Username.Equals(username))
                .Include(x => x.Tags)
                .FirstOrDefaultAsync();
            if (user is null)
                throw new CustomNotFoundException("User not found");
            var tag = await _tagRepository.GetByIdAsync(id);
            if (tag is null)
                throw new CustomNotFoundException("Tag not found");
            if (!user.Tags.Contains(tag))
                throw new CustomBadRequestException("User does not follows this tag");
            user.Tags.Remove(tag);
            await _userRepository.SaveChangesAsync();
        }
    }
}
