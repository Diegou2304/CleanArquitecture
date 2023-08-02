using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;
using CleanArchitecture.Application.Mappings;
using CleanArchitecture.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Application.UnitTests.Features.Videos.Queries
{
    public class GetVideosListQueryHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public GetVideosListQueryHandlerTest()
        {
            _unitOfWork = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();  
        }

        [Fact]
        public async Task GetVideoListTest()
        {
            var handler = new GetVideosListQueryHandler(_mapper, _unitOfWork.Object);
            var request = new GetVideosListQuery("Diego");
            
            var result = await handler.Handle(request, CancellationToken.None);

            result.ShouldBeOfType<List<GetVideosListQueryResponse>>();
            result.Count.ShouldBe(1);
        }
    }
}
