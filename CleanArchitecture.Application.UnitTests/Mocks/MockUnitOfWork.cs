

using CleanArchitecture.Application.Contracts.Persistence;
using Moq;

namespace CleanArchitecture.Application.UnitTests.Mocks
{
    public class MockUnitOfWork
    {
        public static Mock<IUnitOfWork> GetUnitOfWork()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockVideoRepository = MockVideoRepository.GetVideoRepository();

            mockUnitOfWork.Setup(r => r.VideoRepository).Returns(mockVideoRepository.Object);

            return mockUnitOfWork;

        }
    }
}
