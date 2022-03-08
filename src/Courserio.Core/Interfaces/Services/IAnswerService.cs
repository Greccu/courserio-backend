using Courserio.Core.DTOs.Answer;

namespace Courserio.Core.Interfaces.Services;

public interface IAnswerService
{
    Task CreateAsync(AnswerCreateDto questionDto);
}