﻿

using CleanArchitecture.Application.Contracts.Persistence;
using CleanArquitecture.Domain.Common;
using CleanArquitecture.Infrastructure.Persistence;
using System.Collections;

namespace CleanArquitecture.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _repositories;
        private readonly StreamerDbContext _context;

        private IVideoRepository _videoRepository;
        private IStreamerRepository _streamerRepository;

        public IVideoRepository VideoRepository => _videoRepository ??= new VideoRepository(_context);
        public IStreamerRepository StreamerRepository => _streamerRepository ??= new StreamerRepository(_context);

        public UnitOfWork(StreamerDbContext context)
        {
            _context = context;
        }

        //Confirma todas las transacciones que se están usando
        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        //Verifica si la entidad existe dentro del hash table para agregarlo o no
        public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name;

            if(!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(BaseRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(type, repositoryInstance);
            }

            return _repositories[type] as IAsyncRepository<TEntity>;
        }
    }
}
