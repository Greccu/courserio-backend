using AutoMapper;
using Courserio.Core.DTOs;
using Courserio.Core.DTOs.Question;
using Courserio.Core.Filters;
using Courserio.Core.Interfaces.Repositories;
using Courserio.Core.Interfaces.Services;
using Courserio.Core.Models;
using Courserio.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Courserio.Core.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IGenericRepository<Question> _questionRepository;
        private readonly IMapper _mapper;

        public QuestionService(IMapper mapper, IGenericRepository<Question> questionRepository)
        {
            _mapper = mapper;
            _questionRepository = questionRepository;
        }

        public async Task<QuestionDto> CreateAsync(QuestionCreateDto questionDto)
        {
            var question = _mapper.Map<Question>(questionDto);
            question.CreatedAt = DateTime.Now;
            await _questionRepository.AddAsync(question);
            question.User = new User();
            return _mapper.Map<QuestionDto>(question);
        }

        public async Task<PagedResult<QuestionDto>> ListAsync(QuestionFilter filter)
        {
            var query = _questionRepository
                .AsQueryable()
                .Include(x => x.User)
                .Include(x => x.Answers)
                    .ThenInclude(x => x.User)
                .AsQueryable()
                ;
            query = query.Where(q => q.ChapterId == filter.ChapterId);
            return await query.MapToPagedResultAsync(filter, _mapper.Map<QuestionDto>);
        }

        public async Task<ICollection<QuestionDto>> GetByChapterIdAsync(int chapterId)
        {
            var questions = await _questionRepository.AsQueryable()
                .Where(x => x.ChapterId == chapterId)
                .Include(x => x.User)
                .Include(x => x.Answers)
                .ThenInclude(x => x.User)
                .ToListAsync();

            var mappedQuestions = questions.Select(_mapper.Map<QuestionDto>).ToList();
            return mappedQuestions;

        }

    }
}