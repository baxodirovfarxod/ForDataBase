using MusicCrudAsync.DataAccess.Entity;

namespace MusicCrudAsync.Repository.Service;
public interface IMusicRepository
{
    Task<Guid> AddMusicAsync(Music music);
    Task<Music> GetByIdAsync(Guid id);
    Task UpdateMusicAsync(Music music);
    Task DeleteMusicAsync(Guid id);
    Task<List<Music>> GetAllMusicAsync();
}
