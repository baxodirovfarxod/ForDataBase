using MusicCrudAsync.DataAccess.Entity;
using MusicCrudAsync.Repository.Service;
using MusicCrudAsync.Service.Dtos;

namespace MusicCrudAsync.Service.Service;
public class MusicService : IMusicService
{
    private readonly IMusicRepository _musicRepository;
    private readonly Func<MusicDto, Music> _convertToEntity;
    private readonly Func<Music, MusicDto> _convertToDto;
    public MusicService(IMusicRepository musicRepository)
    {
        _musicRepository = musicRepository;
        _convertToEntity = music => new Music
        {
            Id = music.Id ?? Guid.NewGuid(),
            AuthorName = music.AuthorName,
            Name = music.Name,
            MB = music.MB,
            Description = music.Description,
            QuentityLikes = music.QuentityLikes
        };
        _convertToDto = music => new MusicDto
        {
            Id = music.Id,
            AuthorName = music.AuthorName,
            Name = music.Name,
            MB = music.MB,
            Description = music.Description,
            QuentityLikes = music.QuentityLikes
        };
    }

    public async Task<Guid> AddMusicAsync(MusicDto music)
    {
        return await _musicRepository.AddMusicAsync(_convertToEntity(music));
    }
    public async Task DeleteMusicAsync(Guid id)
    {
        await _musicRepository.DeleteMusicAsync(id);
    }
    public async Task<List<MusicDto>> GetAllMusicAsync()
    {
        var music = await _musicRepository.GetAllMusicAsync();
        return music.Select(m => _convertToDto(m)).ToList();
    }
    public async Task UpdateMusicAsync(MusicDto music)
    {
        await _musicRepository.UpdateMusicAsync(_convertToEntity(music));
    }
    public async Task<List<MusicDto>> GetAllMusicByAuthorNameAsync(string name)
    {
        var musicList = await GetAllMusicAsync();
        return musicList.Where(mu => mu.AuthorName.ToLower().Contains(name.ToLower())).ToList();
    }
    public async Task<MusicDto> GetMostLikedMusicAsync()
    {
        var musicList = await GetAllMusicAsync();
        var maxLike = musicList.Max(mu => mu.QuentityLikes);
        return musicList.First(mu => mu.QuentityLikes == maxLike);
    }
    public async Task<MusicDto> GetMusicByNameAsync(string name)
    {
        var musicList = await GetAllMusicAsync();
        return musicList.SingleOrDefault(mu => mu.Name.ToLower() == name.ToLower()) 
            ?? throw new Exception($"{name} music not found");
    }

}
