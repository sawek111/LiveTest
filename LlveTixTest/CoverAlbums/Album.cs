using System.Text.Json.Serialization;

namespace LlveTixTest.CoverAlbums;

public record Album
{
    public int UserId { get; init; }
    public int Id { get; init; }
    public string? Title { get; init; }
}