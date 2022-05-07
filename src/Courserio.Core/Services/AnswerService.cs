using AutoMapper;
using Courserio.Core.DTOs.Answer;
using Courserio.Core.Interfaces.Repositories;
using Courserio.Core.Interfaces.Services;
using Courserio.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Courserio.Core.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IGenericRepository<Answer> _answerRepository;
        private readonly IMapper _mapper;

        public AnswerService(IMapper mapper, IGenericRepository<Answer> answerRepository)
        {
            _mapper = mapper;
            _answerRepository = answerRepository;
        }

        public async Task<AnswerDto> CreateAsync(AnswerCreateDto answerDto)
        {
            var answer = _mapper.Map<Answer>(answerDto);
            answer.CreatedAt = DateTime.Now;
            await _answerRepository.AddAsync(answer);
            return _mapper.Map<AnswerDto>(answer);
        }
    }
}