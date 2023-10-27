using System.Net.Http.Json;

namespace ClientComponent;

public class ClientComponent<TKey, TValue>: IClientComponent<TKey, TValue>
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public ClientComponent(string baseUrl)
    {
        _baseUrl = baseUrl;
        var clientHandler = new HttpClientHandler();
        clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
        _httpClient = new HttpClient(clientHandler);
    }

    public async Task<KeyValuePair<TKey, TValue>?> Get(TKey key)
    {
        using var response = await _httpClient.GetAsync($"{_baseUrl}/keys/get/{key}");
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadFromJsonAsync<KeyValuePair<TKey, TValue>>();
        return responseBody;
    }


    public async Task<KeyValuePair<TKey, TValue>?> Set(TKey key, TValue value)
    {
        using var response = await _httpClient.PutAsJsonAsync(
            $"{_baseUrl}/keys/set/{key}/{value}",
            new KeyValuePair<TKey, TValue>(key, value));
        response.EnsureSuccessStatusCode();
        var pair = await response.Content.ReadFromJsonAsync<KeyValuePair<TKey, TValue>>();
        return pair;
    }
}