using MusicCrudAsync.DataAccess.Entity;
using MusicCrudAsync.Service.Dtos;

namespace MusicCrudAsync.Service.Service;
public interface IMusicService
{
    Task<Guid> AddMusicAsync(MusicDto music);
    Task UpdateMusicAsync(MusicDto music);
    Task DeleteMusicAsync(Guid id);
    Task<List<MusicDto>> GetAllMusicAsync();
    Task<List<MusicDto>> GetAllMusicByAuthorNameAsync(string name);
    Task<MusicDto> GetMostLikedMusicAsync();
    Task<MusicDto> GetMusicByNameAsync(string name);
}
