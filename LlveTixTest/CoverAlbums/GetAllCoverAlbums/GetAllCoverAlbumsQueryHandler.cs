using LlveTixTest.CoverAlbums.PlaceHolder;
using MediatR;
using Microsoft.Extensions.Options;

namespace LlveTixTest.CoverAlbums.GetAllCoverAlbums;

internal sealed class GetAllCoverAlbumsQueryHandler : IRequestHandler<GetAllCoverAlbumsQuery, IEnumerable<CoverAlbum>>
{
    private readonly IPlaceHolderService _placeHolderService;

    public GetAllCoverAlbumsQueryHandler(IPlaceHolderService placeHolderService)
    {
        _placeHolderService = placeHolderService;
    }

    public async Task<IEnumerable<CoverAlbum>> Handle(GetAllCoverAlbumsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var (albums, photos) = await GetDataAsync();
            var coverAlbums = new List<CoverAlbum>();
            var photosByAlbumId = photos.GroupBy(p => p.AlbumId).ToDictionary(g => g.Key, g => g.ToList());

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

    private async Task<(IEnumerable<Album> albums, IEnumerable<Photo> photos)> GetDataAsync()
    {
        var albumsTask = _placeHolderService.GetAlbums();
        var photosTask = _placeHolderService.GetPhotos();
        await Task.WhenAll(albumsTask, photosTask);
        return (albumsTask.Result, photosTask.Result);
    }
}