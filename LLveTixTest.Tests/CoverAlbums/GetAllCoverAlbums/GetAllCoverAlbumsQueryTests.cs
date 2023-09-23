using FluentAssertions;
using LlveTixTest.CoverAlbums;
using LlveTixTest.CoverAlbums.GetAllCoverAlbums;
using Moq;

namespace LLveTixTest.Tests.CoverAlbums.GetAllCoverAlbums;

public class GetAllCoverAlbumsQueryHandlerTests
{
    private readonly Mock<IPlaceHolderService> _placeHolderServiceMock;


    public GetAllCoverAlbumsQueryHandlerTests()
    {
        _placeHolderServiceMock = new Mock<IPlaceHolderService>();
    }

    [Fact]
    public async Task Handle_Returns_CoverAlbums()
    {
        var albums = Factory.GenerateAlbums(5);
        var photos = Factory.GeneratePhotosForAlbums(albums, 10);
        _placeHolderServiceMock.Setup(s => s.GetAlbums()).ReturnsAsync(albums);
        _placeHolderServiceMock.Setup(s => s.GetPhotos()).ReturnsAsync(photos);
        var query = new GetAllCoverAlbumsQuery();
        var handler = new GetAllCoverAlbumsQueryHandler(_placeHolderServiceMock.Object);

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
        _placeHolderServiceMock.Setup(s => s.GetAlbums()).ThrowsAsync(new Exception(string.Empty));
        var query = new GetAllCoverAlbumsQuery();
        var handler = new GetAllCoverAlbumsQueryHandler(_placeHolderServiceMock.Object);
        
        var result = await handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }
}