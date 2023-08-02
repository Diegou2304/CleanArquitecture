

using AutoFixture;
using CleanArquitecture.Domain;
using CleanArquitecture.Infrastructure.Persistence;
using CleanArquitecture.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CleanArchitecture.Application.UnitTests.Mocks
{
    public static class MockVideoRepository
    {
        public static Mock<VideoRepository> GetVideoRepository()
        {
            var fixture = new Fixture();
            //Omite la recursion circular de las relaciones
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var videos = fixture.CreateMany<Video>().ToList();

            videos.Add(fixture.Build<Video>()
                .With(tr => tr.CreatedBy, "vaxidrez")
                .Create()
                );

            var options = new DbContextOptionsBuilder<StreamerDbContext>()
                .UseInMemoryDatabase(databaseName: $"StreamerDbContext-{Guid.NewGuid}")
                .Options;

            var streamerDbContextFake = new StreamerDbContext(options);
            streamerDbContextFake.Videos.AddRange(videos.ToList());
            streamerDbContextFake.SaveChanges();

            var mockRepository = new Mock<VideoRepository>(streamerDbContextFake);

       
            return mockRepository;
        }
    }
}
