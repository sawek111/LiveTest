using Bogus;
using LlveTixTest.CoverAlbums;

namespace LLveTixTest.Tests.CoverAlbums;

public static class Factory
{
    internal static IEnumerable<Photo> GeneratePhotosForAlbums(IReadOnlyList<Album> albums, int count)
    {
        var photos = new Faker<Photo>()
            .RuleFor(p => p.Id, f => f.Random.Int())
            .RuleFor(p => p.AlbumId, f => albums[f.Random.Int(0, albums.Count - 1)].Id)
            .Generate(count);
        return photos;
    }

    internal static List<Album> GenerateAlbums(int count, int? userId = null)
    {
        var albums = new Faker<Album>()
            .RuleFor(a => a.Id, f => f.Random.Int())
            .RuleFor(a => a.Title, f => f.Random.Word())
            .RuleFor(a => a.UserId, f => userId ?? f.Random.Int())
            .Generate(count);
        return albums;
    }
}