using AutoMapper;
using Courserio.Core.DTOs.Tags;
using Courserio.Core.Interfaces.Repositories;
using Courserio.Core.Interfaces.Services;
using Courserio.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Courserio.Core.Services
{
    public class TagService : ITagService
    {
        private readonly IGenericRepository<Tag> _tagRepository;
        private readonly IMapper _mapper;

        public TagService(IGenericRepository<Tag> tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public async Task<List<TagDto>> ListAsync()
        {
            var tags = await _tagRepository.AsQueryable().ToListAsync();
            return tags.Select(_mapper.Map<TagDto>).ToList();
        }

        public async Task CreateManyAsync(List<string> tags)
        {
            var tagsToAdd = tags.Select(_mapper.Map<Tag>).ToList();
            await _tagRepository.AddRangeAsync(tagsToAdd);
        }
    }
}
