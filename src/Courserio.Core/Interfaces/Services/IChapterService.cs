using Courserio.Core.DTOs.Chapter;

namespace Courserio.Core.Interfaces.Services
{
    public interface IChapterService
    {
        Task CreateAsync(ChapterCreateDto chapterDto);
        Task<ChapterPageDto> GetByIdAsync(int id);
    }
}
