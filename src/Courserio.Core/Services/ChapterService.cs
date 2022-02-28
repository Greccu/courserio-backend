using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Courserio.Core.DTOs.Chapter;
using Courserio.Core.DTOs.Course;
using Courserio.Core.Interfaces.Repositories;
using Courserio.Core.Interfaces.Services;
using Courserio.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Courserio.Core.Services
{
    public class ChapterService : IChapterService
    {
        private readonly IGenericRepository<Chapter> _chapterRepository;
        private readonly IMapper _mapper;

        public ChapterService( IMapper mapper, IGenericRepository<Chapter> chapterRepository)
        {
            _mapper = mapper;
            _chapterRepository = chapterRepository;
        }

        public async Task CreateAsync(ChapterCreateDto chapterDto)
        {
            var chapter = _mapper.Map<Chapter>(chapterDto);
            chapter.CreatedAt = DateTime.Now;
            await _chapterRepository.AddAsync(chapter);
        }


        public async Task<ChapterPageDto> GetByIdAsync(int id)
        {
            var course = await _chapterRepository
                    .AsQueryable()
                    .Where(x => x.Id == id)
                    .Include(x => x.Questions)
                    .ThenInclude(x => x.Answers)
                    .ThenInclude(x => x.User)
                    .FirstOrDefaultAsync()
                ;
            return _mapper.Map<ChapterPageDto>(course);
        }
    }
}
