using Courserio.Core.DTOs;
using Courserio.Core.DTOs.Question;
using Courserio.Core.Filters;

namespace Courserio.Core.Interfaces.Services;

public interface IQuestionService
{
    Task<ICollection<QuestionDto>> GetByChapterIdAsync(int chapterId);
    Task<QuestionDto> CreateAsync(QuestionCreateDto questionDto);
    Task<PagedResult<QuestionDto>> ListAsync(QuestionFilter filter);
}

