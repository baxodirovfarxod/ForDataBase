using MusicCrudAsync.DataAccess.Entity;
using System.Text.Json;

namespace MusicCrudAsync.Repository.Service;
public class MusicRepositoryFile : IMusicRepository
{
    private readonly string _filePath;
    private List<Music> _music;

    public MusicRepositoryFile()
    {
        var _directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Data");
        _filePath = Path.Combine(_directoryPath, "Music.json");

        if (!Directory.Exists(_directoryPath))
        {
            Directory.CreateDirectory(_directoryPath);
        }

        if (!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, "[]");
        }

        _music = GetAllAsync().Result;
    }

    public async Task<Guid> AddMusicAsync(Music music)
    {
        _music.Add(music);
        await SaveAllAsync();

        return music.Id;
    }
    public async Task<Music> GetByIdAsync(Guid id)
    {
        var musicFromDB = _music.FirstOrDefault(mus => mus.Id == id);
        if (musicFromDB == null) 
            throw new Exception($"{id} lik musiqa topilmadi!");
        
        return musicFromDB;
    }
    public async Task DeleteMusicAsync(Guid id)
    {
        var musicFromDB = await GetByIdAsync(id);
        _music.Remove(musicFromDB);
        await SaveAllAsync();
    }
    public async Task UpdateMusicAsync(Music music)
    {
        var index = _music.IndexOf(await GetByIdAsync(music.Id));
        _music[index] = music;
        await SaveAllAsync();
    }
    public async Task<List<Music>> GetAllMusicAsync()
    {
        return await GetAllAsync();
    }

    private async Task SaveAllAsync()
    {
        var musicJson = JsonSerializer.Serialize(_music);
        await File.WriteAllTextAsync(_filePath, musicJson);
    }
    private async Task<List<Music>> GetAllAsync()
    {
        var musicJson = await File.ReadAllTextAsync(_filePath);
        return JsonSerializer.Deserialize<List<Music>>(musicJson) ?? new List<Music>();
    }
}
