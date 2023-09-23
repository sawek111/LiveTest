using LlveTixTest.CoverAlbums.GetUserCoverAlbums;
using FluentAssertions;
using LlveTixTest.CoverAlbums;
using Moq;

namespace LLveTixTest.Tests.CoverAlbums.GetAllCoverAlbums;

public class GetUserCoverAlbumsQueryHandlerTests
{
    private readonly Mock<IPlaceHolderService> _placeHolderServiceMock;

    public GetUserCoverAlbumsQueryHandlerTests()
    {
        _placeHolderServiceMock = new Mock<IPlaceHolderService>();
    }

    [Fact]
    public async Task Handle_Returns_CoverAlbums()
    {
        const int userId = 1;
        var albums = Factory.GenerateAlbums(5);
        var photos = Factory.GeneratePhotosForAlbums(albums, 10);
        _placeHolderServiceMock.Setup(s => s.GetAlbums(userId)).ReturnsAsync(albums);
        _placeHolderServiceMock.Setup(s => s.GetPhotos(userId)).ReturnsAsync(photos);
        var handler = new GetUserCoverAlbumsQueryHandler(_placeHolderServiceMock.Object);
        var query = new GetUserCoverAlbumsQuery(userId);

        var result = await handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().HaveCount(albums.Count);

        foreach (var coverAlbum in result)
        {
            coverAlbum.Id.Should().NotBe(Guid.Empty);
            coverAlbum.Title.Should().NotBeNullOrEmpty();
            coverAlbum.Photos.Should().NotBeNull();
        }
    }

    [Fact]
    public async Task Handle_Returns_EmptyList_OnException()
    {
        const int userId = 1;
        _placeHolderServiceMock.Setup(s => s.GetAlbums(userId)).ThrowsAsync(new Exception(string.Empty));
        var handler = new GetUserCoverAlbumsQueryHandler(_placeHolderServiceMock.Object);
        var query = new GetUserCoverAlbumsQuery(userId);

        var result = await handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }
}