using Courserio.Core.DTOs.Question;

namespace Courserio.Core.Interfaces.Services;

public interface IQuestionService
{
    Task<ICollection<QuestionDto>> GetByChapterIdAsync(int chapterId);
    Task CreateAsync(QuestionCreateDto questionDto);
}

