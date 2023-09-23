using MediatR;

namespace LlveTixTest.CoverAlbums.GetUserCoverAlbums;

internal sealed class GetUserCoverAlbumsQueryHandler : IRequestHandler<GetUserCoverAlbumsQuery, IEnumerable<CoverAlbum>>
{
    private readonly IPlaceHolderService _placeHolderService;

    public GetUserCoverAlbumsQueryHandler(IPlaceHolderService placeHolderService)
    {
        _placeHolderService = placeHolderService;
    }

    public async Task<IEnumerable<CoverAlbum>> Handle(GetUserCoverAlbumsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var (albums, photos) = await GetDataAsync(request.UserId);
            var photosByAlbumId = photos.GroupBy(p => p.AlbumId).ToDictionary(g => g.Key, g => g.ToList());
            var coverAlbums = new List<CoverAlbum>();

            foreach (var album in albums)
            {
                coverAlbums.Add(CoverAlbum.Create(album, photosByAlbumId.GetValueOrDefault(album.Id) ?? Enumerable.Empty<Photo>()));
            }

            return coverAlbums;
        }
        catch (Exception)
        {
            return Enumerable.Empty<CoverAlbum>();
        }
    }

    private async Task<(IEnumerable<Album> albums, IEnumerable<Photo> photos)> GetDataAsync(int? userId)
    {
        var albumsTask = userId is not null ? _placeHolderService.GetAlbums(userId.Value) : _placeHolderService.GetAlbums();
        var photosTask = userId is not null ? _placeHolderService.GetPhotos(userId.Value) : _placeHolderService.GetPhotos();
        await Task.WhenAll(albumsTask, photosTask);
        return (albumsTask.Result, photosTask.Result);
    }
}