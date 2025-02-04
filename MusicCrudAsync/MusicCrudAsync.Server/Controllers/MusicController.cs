using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicCrudAsync.Service.Dtos;
using MusicCrudAsync.Service.Service;

namespace MusicCrudAsync.Server.Controllers
{
    [Route("api/music")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private IMusicService _musicService;
        public MusicController(IMusicService musicService)
        {
            _musicService = musicService;
        }

        [HttpPost("add")]
        public async Task<Guid> AddMusic(MusicDto music)
        {
            return await _musicService.AddMusicAsync(music);
        }

        [HttpPut("update")]
        public async Task UpdateMusic(MusicDto music)
        {
            await _musicService.UpdateMusicAsync(music);
        }

        [HttpDelete("delete")]
        public async Task DeleteMusic(Guid id)
        {
            await _musicService.DeleteMusicAsync(id);
        }

        [HttpGet("getAll")]
        public async Task<List<MusicDto>> GetAllMusic()
        {
            return await _musicService.GetAllMusicAsync();
        }
        [HttpGet("GetAllByAuthorName")]
        public async Task<List<MusicDto>> GetAllByAuthorName(string name)
        {
            return await _musicService.GetAllMusicByAuthorNameAsync(name);
        }

        [HttpGet("getMostLiked")]
        public async Task<MusicDto> GetMostLikedMusic()
        {
            return await _musicService.GetMostLikedMusicAsync();
        }

        [HttpGet("getByName")]
        public async Task<MusicDto> GetMusicByName(string name)
        {
            return await _musicService.GetMusicByNameAsync(name);
        }
    }
}
