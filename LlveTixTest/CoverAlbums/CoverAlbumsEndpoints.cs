using LlveTixTest.CoverAlbums.GetAllCoverAlbums;
using LlveTixTest.CoverAlbums.GetUserCoverAlbums;
using MediatR;

namespace LlveTixTest.CoverAlbums;

public static class CoverAlbumsEndpoints
{
    private const string MainRoute = "api/coverAlbums";

    public static void AddCoverAlbumsEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(MainRoute);
        
        group.MapGet(string.Empty, GetAllCovers);
        group.MapGet("{userId}", GetUserCovers);
    }

    private static async Task<IResult> GetAllCovers(ISender sender)
    {
        var response = await sender.Send(new GetAllCoverAlbumsQuery());
        return Results.Ok(response);
    }

    private static async Task<IResult> GetUserCovers(int userId, ISender sender)
    {
        var response = await sender.Send(new GetUserCoverAlbumsQuery(userId));
        return Results.Ok(response);
    }
}