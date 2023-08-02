using CleanArchitecture.Application.Contracts.Persistence;
using CleanArquitecture.Domain;
using CleanArquitecture.Infrastructure.Persistence;

namespace CleanArquitecture.Infrastructure.Repositories
{
    public class StreamerRepository : BaseRepository<Streamer>, IStreamerRepository
    {

        public StreamerRepository(StreamerDbContext context) : base(context) 
        {
            
        }

    }
}
