using CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

namespace CleanArchitecture.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class VideoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VideoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{username}", Name = "GetVideo")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<GetVideosListQueryResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<GetVideosListQueryResponse>>> GetVideosByUsername(string username)
        {
            //El query esta linkeado con el handler del command o del query
            var query = new GetVideosListQuery(username);
            var videos = await _mediator.Send(query);

            return Ok(videos);

        }

    }
}
