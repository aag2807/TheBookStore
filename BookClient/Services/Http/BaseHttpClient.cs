using System.Net.Http.Json;
using System.Reactive;
using System.Reactive.Linq;
using System.Text.Json;

namespace BookClient.Services.Http;

/// <summary>
/// Base class from which all http client services will inherit from
/// </summary>
abstract class BaseHttpClient : IBaseHttpClient
{
    private readonly HttpClient _httpClient;

    protected BaseHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <inheritdoc />
    IObservable<T> IBaseHttpClient.GetAsync<T>(string url)
    {
        return Observable.FromAsync(async () =>
        {
            var response = await _httpClient.GetAsync(url).ConfigureAwait(false);
            return await ProcessHttpClientResponse<T>(response);
        });
    }

    /// <inheritdoc />
    IObservable<T> IBaseHttpClient.PostAsync<T>(string url, T content)
    {
        return Observable.FromAsync(async () =>
        {
            var response = await _httpClient.PostAsJsonAsync(url, content).ConfigureAwait(false);
            return await ProcessHttpClientResponse<T>(response);
        });
    }

    /// <inheritdoc />
    IObservable<T> IBaseHttpClient.PutAsync<T>(string url, T content)
    {
        return Observable.FromAsync(async () =>
        {
            var response = await _httpClient.PutAsJsonAsync(url, content).ConfigureAwait(false);
            return await ProcessHttpClientResponse<T>(response);
        });
    }

    /// <inheritdoc />
    IObservable<T> IBaseHttpClient.PatchAsync<T>(string url, HttpContent content)
    {
        return Observable.FromAsync(async () =>
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, url) { Content = content };
            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            return await ProcessHttpClientResponse<T>(response);
        });
    }

    /// <inheritdoc />
    IObservable<Unit> IBaseHttpClient.DeleteAsync(string url)
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