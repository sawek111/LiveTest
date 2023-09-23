using System.Text.Json;

namespace LlveTixTest.CoverAlbums;

public sealed class PlaceHolderService : IPlaceHolderService
{
    private readonly HttpClient _httpClient;


    public PlaceHolderService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Album>> GetAlbums()
    {
        const string urlPostfix = "albums";
        try
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<Album>>(urlPostfix);
            return response ?? Enumerable.Empty<Album>();
        }
        catch (Exception e)
        {
            return Enumerable.Empty<Album>();
        }
    }
    
    public async Task<IEnumerable<Album>> GetAlbums(int userId)
    {
        var urlPostfix = $"albums?userId={userId}";
        try
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<Album>>(urlPostfix);
            return response ?? Enumerable.Empty<Album>();
        }
        catch (Exception e)
        {
            return Enumerable.Empty<Album>();
        }
    }

    public async Task<IEnumerable<Photo>> GetPhotos()
    {
        const string urlPostfix = "photos";
        try
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<Photo>>(urlPostfix);
            return response ?? Enumerable.Empty<Photo>();
        }
        catch (Exception e)
        {
            return Enumerable.Empty<Photo>();
        }
    }
    
    public async Task<IEnumerable<Photo>> GetPhotos(int userId)
    {
        var urlPostfix = $"photos?userId={userId}";
        try
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<Photo>>(urlPostfix);
            return response ?? Enumerable.Empty<Photo>();
        }
        catch (Exception e)
        {
            return Enumerable.Empty<Photo>();
        }
    }
}