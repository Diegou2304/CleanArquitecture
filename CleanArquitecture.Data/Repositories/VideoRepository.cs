

using CleanArchitecture.Application.Contracts.Persistence;
using CleanArquitecture.Domain;
using CleanArquitecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanArquitecture.Infrastructure.Repositories
{
    public class VideoRepository : BaseRepository<Video>, IVideoRepository
    {
        public VideoRepository(StreamerDbContext context) : base(context)
        {
        }

        public async Task<Video> GetVideoByName(string videoName)
        {
            
            return await _context.Videos!.Where(v => v.Name == videoName).FirstOrDefaultAsync();

        }

        public async Task<IEnumerable<Video>> GetVideoByUsername(string username)
        {
            return await _context.Videos.Where(v => v.CreatedBy == username).ToListAsync();
        }
    }
}
