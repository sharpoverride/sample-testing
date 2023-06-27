namespace Sample.Core;

using System.Text.Json;

public class SampleClient: ISampleClient
{
    private readonly HttpClient _client;

    public SampleClient(HttpClient client)
    {
        _client = client;
    }
    
    public async Task<SampleEntity> GetSampleEntityAsync(Guid uuid)
    {
        var response = await _client.GetAsync($"/api/sample/{uuid}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<SampleEntity>(content)!;
    }
}

public interface ISampleClient
{
    Task<SampleEntity> GetSampleEntityAsync(Guid uuid);
}