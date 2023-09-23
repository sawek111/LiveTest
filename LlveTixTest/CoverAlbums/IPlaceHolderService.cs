namespace LlveTixTest.CoverAlbums;

public interface IPlaceHolderService
{
    Task<IEnumerable<Album>> GetAlbums();
    Task<IEnumerable<Album>> GetAlbums(int userId);
    Task<IEnumerable<Photo>> GetPhotos();
    Task<IEnumerable<Photo>> GetPhotos(int userId);
}