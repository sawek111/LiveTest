using Microsoft.AspNetCore.Http.HttpResults;

namespace LlveTixTest.CoverAlbums;

public class CoverAlbum
{
    public Guid Id { get; private set; } 
    public int UserId { get; private set; }
    public int AlbumId { get; private set; }
    public string? Title { get; private set; }
    public IEnumerable<Photo> Photos { get; private set; }
    
    public static CoverAlbum? Create(Album? album, IEnumerable<Photo> photos, Guid? guid = null)
    {
        if (album == null)
        {
            return null;
        }
        
        return new CoverAlbum()
        {
            Id = guid ?? Guid.NewGuid(),
            Title = album.Title,
            AlbumId = album.Id,
            UserId = album.UserId,
            Photos = photos
        };
    }
}
