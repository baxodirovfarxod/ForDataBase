using Microsoft.EntityFrameworkCore;
using MusicCrudAsync.DataAccess;
using MusicCrudAsync.DataAccess.Entity;


namespace MusicCrudAsync.Repository.Service;
public class MusicRepositoryDB : IMusicRepository
{
    private readonly MainContext _mainContext;
    public MusicRepositoryDB(MainContext mainContext)
    {
        _mainContext = mainContext;
    }
    public async Task<Guid> AddMusicAsync(Music music)
    {
        await _mainContext.Music.AddAsync(music);
        await _mainContext.SaveChangesAsync();

        return music.Id;
    }

    public async Task DeleteMusicAsync(Guid id)
    {
        var music = await GetByIdAsync(id);
        _mainContext.Music.Remove(music);
        await _mainContext.SaveChangesAsync();
    }

    public async Task<List<Music>> GetAllMusicAsync()
    {
        var allMusic = await _mainContext.Music.ToListAsync();

        return allMusic;
    }

    public async Task<Music> GetByIdAsync(Guid id)
    {
        var music = await _mainContext.Music.FirstOrDefaultAsync(mu => mu.Id == id);
        if (music == null)
            throw new Exception($"{id} lik music topilmadi !");

        return music;
    }

    public async Task UpdateMusicAsync(Music music)
    {
        var existingMusic = await _mainContext.Music.FirstOrDefaultAsync(mu => mu.Id == music.Id);
        if (existingMusic == null)
            throw new Exception($"Music with ID {music.Id} not found.");

        existingMusic.Name = music.Name;
        existingMusic.MB = music.MB;
        existingMusic.AuthorName = music.AuthorName;
        existingMusic.Description = music.Description;
        existingMusic.QuentityLikes = music.QuentityLikes;

        await _mainContext.SaveChangesAsync();
    }
}
