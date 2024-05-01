using System.Net.Http.Json;
using System.Reactive;
using System.Reactive.Linq;
using System.Text.Json;

namespace BookClient.Services.Http;

/// <summary>
/// Base class from which all http client services will inherit from
/// </summary>
public abstract class BaseHttpClient
{
    private readonly HttpClient _httpClient;

    protected BaseHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    protected IObservable<T> GetAsync<T>(string url)
    {
        return Observable.FromAsync(async () =>
        {
            var response = await _httpClient.GetAsync(url).ConfigureAwait(false);
            return await ProcessHttpClientResponse<T>(response);
        });
    }

    protected IObservable<K> PostAsync<T, K>(string url, T content)
    {
        return Observable.FromAsync(async () =>
        {
            var response = await _httpClient.PostAsJsonAsync(url, content).ConfigureAwait(false);
            return await ProcessHttpClientResponse<K>(response);
        });
    }

    protected IObservable<T> PutAsync<T>(string url, T content)
    {
        return Observable.FromAsync(async () =>
        {
            var response = await _httpClient.PutAsJsonAsync(url, content).ConfigureAwait(false);
            return await ProcessHttpClientResponse<T>(response);
        });
    }

    protected IObservable<T> PatchAsync<T>(string url, HttpContent content)
    {
        return Observable.FromAsync(async () =>
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, url) { Content = content };
            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            return await ProcessHttpClientResponse<T>(response);
        });
    }

    protected IObservable<Unit> DeleteAsync(string url)
    {
        return Observable.FromAsync(async () =>
        {
            var response = await _httpClient.DeleteAsync(url).ConfigureAwait(false);
            await ProcessHttpClientResponse<Unit>(response);
            return Unit.Default;
        });
    }

    private async Task<T> ProcessHttpClientResponse<T>(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Error fetching data: {response.StatusCode}");
        }

        var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
    }
}