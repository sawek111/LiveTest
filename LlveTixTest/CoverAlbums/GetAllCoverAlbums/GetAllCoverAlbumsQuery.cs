using MediatR;

namespace LlveTixTest.CoverAlbums.GetAllCoverAlbums;

internal record GetAllCoverAlbumsQuery() : IRequest<IEnumerable<CoverAlbum>>
{
}