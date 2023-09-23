using MediatR;

namespace LlveTixTest.CoverAlbums.GetUserCoverAlbums;

internal record GetUserCoverAlbumsQuery(int? UserId) : IRequest<IEnumerable<CoverAlbum>>
{
}