using MediatR;

namespace CleanArchitecture.Application.Features.Videos.Queries.GetVideosList
{
    public class GetVideosListQuery : IRequest<List<GetVideosListQueryResponse>>
    {
        public string Username { get; set; } = String.Empty;

        public GetVideosListQuery (string? username)
        {
            Username = username ?? throw new ArgumentNullException(nameof(username));
        }

    }
}
